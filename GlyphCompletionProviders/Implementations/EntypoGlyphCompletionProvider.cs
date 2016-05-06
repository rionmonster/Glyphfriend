using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Entypo")]
    class EntypoGlyphCompletionEntryGlyphProvider : BaseGlyphfriendProvider
    {
        public EntypoGlyphCompletionEntryGlyphProvider()
        {
            // Define the library name, used to determine the proper folder for the glyphs
            Library = "Entypo";
            // Define the pattern used to match with the CSS file that defines the glyphs
            GlyphCSSDefinitionExpression = new Regex(@"^entypo?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            // Define an optional default icon name to display if a glyph is unavailable
            DefaultIconClass = "icon-default";
        }
    }
}
