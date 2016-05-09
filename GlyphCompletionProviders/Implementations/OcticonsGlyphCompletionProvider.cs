using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;

namespace Glyphfriend.GlyphCompletionProviders
{

    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Octicons")]
    class OcticonsGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public OcticonsGlyphCompletionProvider()
        {
            // Define the library name, used to determine the proper folder for the glyphs
            Library = "Octicons";
            // Define the pattern used to match with the CSS file that defines the glyphs
            GlyphCSSDefinitionExpression = new Regex(@"^octicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            // TODO: Define an optional default icon name to display if a glyph is unavailable
            DefaultIconClass = "octicon-default";
        }
    }
}
