using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Bootstrap")]
    [Order(Before = "Default Bootstrap")]
    internal class BootstrapGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public BootstrapGlyphCompletionProvider()
        {
            Library = "Bootstrap";
            GlyphCssDefinitionExpression = new Regex(@"^bootstrap(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
    }
}
