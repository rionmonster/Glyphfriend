using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Editor;

namespace Glyphfriend.Helpers
{
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType(MarkdownContentTypeDefinition.MarkdownContentType)]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    class MarkdownTextViewListener : IVsTextViewCreationListener
    {
        // This will be triggered when a Markdown document is opened
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            // If the Emojis haven't been loaded, load them
            if (GlyphfriendPackage.Emojis == null)
            {
                GlyphfriendPackage.LoadEmojis();
            }
        }
    }
}
