using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using System.Collections.Generic;
using System.Linq;
using Glyphfriend.Core;

namespace Glyphfriend.EmojiCompletionProviders
{
    internal class EmojiCompletionSource : ICompletionSource
    {
        private readonly List<Completion> _emojis;
        private readonly ITextStructureNavigator _textStructureNavigator;
        private bool _disposed;

        public EmojiCompletionSource(ITextStructureNavigator textStructureNavigator)
        {
            _textStructureNavigator = textStructureNavigator;
            // Build a collection of Emojis to handle 
            _emojis = GlyphfriendPackage.Emojis
                                        .Select(e => EmojiCompletion(e.Key, e.Value))
                                        .ToList();
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            // If this session has already been disposed, ignore it
            if (_disposed)
            {
                return;
            }
               
            // Build a snapshot off the current buffer along with a trigger point
            ITextSnapshot snapshot = session.TextView.TextBuffer.CurrentSnapshot;
            SnapshotPoint? triggerPoint = session.GetTriggerPoint(snapshot);

            // Ensuer the trigger point is valid
            if (triggerPoint == null || triggerPoint.Value.Position == 0)
            {
                return;
            }
                
            // Build a span based off of the current
            ITrackingSpan tracking = FindTokenSpanAtPosition(session);
            // If we couldn't build a span, ignore this Session
            if (tracking == null)
            {
                return;
            }
                
            // Add all of the emojis to the available completions
            List<Completion> completions = _emojis;
            if (completions.Any())
            {
                // Explicitly remove the HTML completion (as this is for Markdown files)
                completionSets.Clear();
                // Add the Emojis to the completion handler (passing along the current tracking span)
                completionSets.Add(new CompletionSet("Emojis", "Emojis", tracking, completions.OrderBy(c => c.DisplayText), Enumerable.Empty<Completion>()));
            }
        }

        public void Dispose()
        {
            _disposed = true;
        }

        private static Completion EmojiCompletion(string emoji, LazyImage emojiImage)
        {
            // Map a completion object for each Emoji to the appropriate image
            var formattedEmoji = $":{emoji}:";
            // Build a completion for each Emoji
            return new Completion(formattedEmoji, formattedEmoji, formattedEmoji, emojiImage?.Image, formattedEmoji);
        }

        private ITrackingSpan FindTokenSpanAtPosition(IIntellisenseSession session)
        {
            // Look for the nearly start of line or space prior to the starting character and 
            // examine it
            SnapshotPoint currentPoint = session.TextView.Caret.Position.BufferPosition - 1;
            TextExtent extent = _textStructureNavigator.GetExtentOfWord(currentPoint);
            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
        }
    }
}
