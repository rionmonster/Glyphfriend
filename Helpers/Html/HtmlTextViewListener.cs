using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.Helpers.Html
{
    [Export(typeof(IVsTextViewCreationListener))]
    [ContentType("htmlx")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal class HtmlTextViewListener : IVsTextViewCreationListener
    {
        // This will be triggered when an HTML document is opened
        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            // If the Glyphs haven't been loaded, load them
            if (!GlyphfriendPackage.AreGlyphsLoaded)
            {
                GlyphfriendPackage.AreGlyphsLoaded = true;
                System.Threading.Tasks.Task.Run(() =>
                {
                    GlyphfriendPackage.LoadGlyphs();
                });
            }
        }
    }
}
