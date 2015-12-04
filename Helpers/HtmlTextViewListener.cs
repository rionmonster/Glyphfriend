using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Editor;

namespace Glyphfriend.Helpers
{
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType("htmlx")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    class HtmlTextViewListener : IVsTextViewCreationListener
    {
        // This will be triggered when an HTML document is opened
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            // If the Glyphs haven't been loaded, load them
            if(GlyphfriendPackage.Glyphs == null)
            {
                GlyphfriendPackage.LoadGlyphs();
            }

        }
    }
}
