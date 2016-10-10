using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Ionic")]
    internal class IonicGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public IonicGlyphCompletionProvider()
        {
            Library = "Ionic";
            GlyphCssDefinitionExpression = new Regex(@"^ionicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "ion-default";
        }
    }
}
