using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Icomoon")]
    internal class IcoMoonGlyphCompletionEntryGlyphProvider : BaseGlyphfriendProvider
    {
        public IcoMoonGlyphCompletionEntryGlyphProvider()
        {
            Library = "IcoMoon";
            GlyphCssDefinitionExpression = new Regex(@"^(style|icomoon)(\-free)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "icon-default";
        }
    }
}
