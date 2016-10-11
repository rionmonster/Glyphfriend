using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Entypo")]
    internal class EntypoGlyphCompletionEntryGlyphProvider : BaseGlyphfriendProvider
    {
        public EntypoGlyphCompletionEntryGlyphProvider()
        {
            Library = "Entypo";
            GlyphCssDefinitionExpression = new Regex(@"^entypo?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "icon-default";
        }
    }
}
