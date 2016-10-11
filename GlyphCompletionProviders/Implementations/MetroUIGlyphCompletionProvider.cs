using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend MetroUI")]
    internal class MetroUIGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public MetroUIGlyphCompletionProvider()
        {
            Library = "MetroUI";
            GlyphCssDefinitionExpression = new Regex(@"^metro-icons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "mif-default";
        }
    }
}
