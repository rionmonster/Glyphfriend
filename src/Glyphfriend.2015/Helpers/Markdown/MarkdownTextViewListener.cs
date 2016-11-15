using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.Helpers.Markdown
{
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType(MarkdownContentTypeDefinition.MarkdownContentType)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class MarkdownTextViewListener : IVsTextViewCreationListener
    {
        [Import]
        IVsEditorAdaptersFactoryService _adaptersFactory;
        [Import]
        ICompletionBroker _completionBroker;

        // This will be triggered when a Markdown document is opened
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            // If the Emojis haven't been loaded, load them
            if (!GlyphfriendPackage.AreEmojisLoaded)
            {
                GlyphfriendPackage.AreEmojisLoaded = true;
                System.Threading.Tasks.Task.Run(() =>
                {
                    GlyphfriendPackage.LoadEmojis();
                });
            }

            // Set up the Completion handler for Markdown documents
            IWpfTextView view = _adaptersFactory.GetWpfTextView(textViewAdapter);
            CommandFilter filter = new CommandFilter(view, _completionBroker);
            IOleCommandTarget next;
            ErrorHandler.ThrowOnFailure(textViewAdapter.AddCommandFilter(filter, out next));
            filter.Next = next;
        }
    }

    public class CommandFilter : IOleCommandTarget
    {
        private readonly IWpfTextView _textView;
        private readonly ICompletionBroker _broker;
        private ICompletionSession _currentSession;
        public IOleCommandTarget Next { get; set; }

        public CommandFilter(IWpfTextView textView, ICompletionBroker broker)
        {
            _textView = textView;
            _broker = broker;
            _currentSession = null;
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            var handled = false;
            var hresult = VSConstants.S_OK;

            // Handle various pre-processed events based on the type of event that occurred
            if (pguidCmdGroup == VSConstants.VSStd2K)
            {
                switch ((VSConstants.VSStd2KCmdID)nCmdID)
                {
                    // If an autocomplete event was completed (or a word was completed)
                    // handle accordingly and start a session if one doesn't exist
                    case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
                    case VSConstants.VSStd2KCmdID.COMPLETEWORD:
                        handled = Start();
                        break;
                    // If a return or tab was performed, commit the current session (i.e.
                    // display the selected item from the completion menu)
                    case VSConstants.VSStd2KCmdID.RETURN:
                        handled = Complete(false);
                        break;
                    case VSConstants.VSStd2KCmdID.TAB:
                        handled = Complete(true);
                        break;
                    // If a cancel event was performed, stop the current session entirely
                    case VSConstants.VSStd2KCmdID.CANCEL:
                        handled = Cancel();
                        break;
                }
            }

            // Based on how the previous events were handle, continue to process
            if (!handled)
            {
                hresult = Next.Exec(pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
            }

            // After the pre-processing has been performed, evaluate the current state
            // and session
            if (ErrorHandler.Succeeded(hresult))
            {
                if (pguidCmdGroup == VSConstants.VSStd2K)
                {
                    switch ((VSConstants.VSStd2KCmdID)nCmdID)
                    {
                        // If a character was entered, check to see what character
                        // and determine if we should start a session
                        case VSConstants.VSStd2KCmdID.TYPECHAR:
                            // If the current character is a colon, start capturing
                            if (GetTypeChar(pvaIn) == ':')
                            {
                                Start();
                            }
                            // If the Session already exists and a key is pressed
                            // continue to filter the existing session
                            else if (DoesSessionExist)
                            {
                                Filter();
                            }
                            break;
                        case VSConstants.VSStd2KCmdID.BACKSPACE:
                            // If a backspace was pressed, filter the existing session
                            Filter();
                            break;
                    }
                }
            }

            return hresult;
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (pguidCmdGroup == VSConstants.VSStd2K)
            {
                switch ((VSConstants.VSStd2KCmdID)prgCmds[0].cmdID)
                {
                    case VSConstants.VSStd2KCmdID.AUTOCOMPLETE:
                    case VSConstants.VSStd2KCmdID.COMPLETEWORD:
                        prgCmds[0].cmdf = (uint)OLECMDF.OLECMDF_ENABLED | (uint)OLECMDF.OLECMDF_SUPPORTED;
                        return VSConstants.S_OK;
                }
            }

            return Next.QueryStatus(pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        private bool Start()
        {
            // Handle non-existant Sessions
            if (_currentSession != null)
            {
                return false;
            }

            // Get the current position within the buffer and take a snapshot
            SnapshotPoint caret = _textView.Caret.Position.BufferPosition;
            ITextSnapshot snapshot = caret.Snapshot;

            if (caret > 1)
            {
                char prev = snapshot.GetText(caret - 2, 1)[0];
                if (!char.IsWhiteSpace(prev))
                    return false;
            }

            // Generate a session based off of the existing broker and textview
            if (!_broker.IsCompletionActive(_textView))
            {
                _currentSession = _broker.CreateCompletionSession(_textView, snapshot.CreateTrackingPoint(caret, PointTrackingMode.Positive), true);
            }
            else
            {
                _currentSession = _broker.GetSessions(_textView)[0];
            }
            // Wire up a handler to dispose of the session
            _currentSession.Dismissed += (sender, args) => _currentSession = null;

            // Start the new session
            _currentSession.Start();
            return true;
        }

        private bool Complete(bool force)
        {
            // Handle non-existant Sessions
            if (_currentSession == null)
            {
                return false;
            }

            // Based off of the available selection, determine if it should be dismissed (via Dismiss) or
            // output (via Commit)
            if (!_currentSession.SelectedCompletionSet.SelectionStatus.IsSelected && !force)
            {
                _currentSession.Dismiss();
                return false;
            }
            _currentSession.Commit();
            return true;
        }

        private void Filter()
        {
            // Handle non-existant sessions
            if (_currentSession == null)
            {
                return;
            }

            // Recalculate the best match available and filter the existing set
            _currentSession.SelectedCompletionSet.SelectBestMatch();
            _currentSession.SelectedCompletionSet.Recalculate();
            _currentSession.Filter();
        }

        private bool Cancel()
        {
            // Handle non-existant sessions
            if (_currentSession == null)
            {
                return false;
            }

            // Explicitly cancel the current session
            _currentSession.Dismiss();
            return true;
        }

        private bool DoesSessionExist => _currentSession != null;

        private static char GetTypeChar(IntPtr pvaIn)
        {
            // Resolves the current character that was pressed
            return (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);
        }
    }
}
