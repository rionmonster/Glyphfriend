using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders.Implementations
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Material Design")]
    internal class MaterialDesignGlyphCompletionProvider : BaseGlyphfriendProvider
    {
        public MaterialDesignGlyphCompletionProvider()
        {
            Library = "MaterialDesign";
            GlyphCssDefinitionExpression = new Regex(@"^materialdesignicons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            DefaultIconClass = "mdi-default";
        }
    }
}
