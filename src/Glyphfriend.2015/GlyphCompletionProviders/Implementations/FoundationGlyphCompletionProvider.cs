using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Foundation")]
    internal class FoundationGlyphCompletionEntryGlyphProvider : BaseGlyphfriendProvider
    {
        public FoundationGlyphCompletionEntryGlyphProvider()
        {
            Library = "Foundation";
            GlyphCssDefinitionExpression = new Regex(@"^foundation-icons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "fi-default";
        }
    }
}
