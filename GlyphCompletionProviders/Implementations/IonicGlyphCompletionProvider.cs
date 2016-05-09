using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Ionic")]
    class IonicGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public IonicGlyphCompletionProvider()
        {
            // Define the library name, used to determine the proper folder for the glyphs
            Library = "Ionic";
            // Define the pattern used to match with the CSS file that defines the glyphs
            GlyphCSSDefinitionExpression = new Regex(@"^ionicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            // Define an optional default icon name to display if a glyph is unavailable
            DefaultIconClass = "ion-default";
        }
    }
}
