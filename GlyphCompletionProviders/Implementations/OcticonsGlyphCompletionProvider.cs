using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{

    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Octicons")]
    internal class OcticonsGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public OcticonsGlyphCompletionProvider()
        {
            Library = "Octicons";
            GlyphCssDefinitionExpression = new Regex(@"^octicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "octicon-default";
        }
    }
}
