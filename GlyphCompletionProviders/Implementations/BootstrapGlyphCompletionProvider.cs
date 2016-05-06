using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Bootstrap")]
    [Order(Before = "Default Bootstrap")]
    class BootstrapGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public BootstrapGlyphCompletionProvider()
        {
            // Define the library name, used to determine the proper folder for the glyphs
            Library = "Bootstrap";
            // Define the pattern used to match with the CSS file that defines the glyphs
            GlyphCSSDefinitionExpression = new Regex(@"^bootstrap(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
