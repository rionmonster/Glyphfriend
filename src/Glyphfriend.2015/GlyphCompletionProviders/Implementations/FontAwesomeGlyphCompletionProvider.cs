using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Font Awesome")]
    internal class FontAwesomeGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public FontAwesomeGlyphCompletionProvider()
        {
            Library = "FontAwesome";
            GlyphCssDefinitionExpression = new Regex(@"^font(-?)awesome(-.*)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "fa-default";
        }
    }
}
