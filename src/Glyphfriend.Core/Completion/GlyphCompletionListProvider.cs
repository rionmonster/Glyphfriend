using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using System.Collections.Generic;
using System.Linq;
using Preferences = Glyphfriend.GlyphfriendPreferences;

namespace Glyphfriend
{
    [HtmlCompletionProvider(CompletionTypes.Values, "*", "class")]
    [ContentType("htmlx")]
    internal class GlyphCompletionListProvider : BaseHtmlCompletionListProvider
    {
        public override string CompletionType { get { return CompletionTypes.Values; } }

        public override IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            var glyphCompletionItems = new List<HtmlCompletion>();

            // Get the filtered set of enabled glyphs
            var enabledGlyphs = Preferences.Glyphs.Where(g => g.Enabled);
            foreach (var glyph in enabledGlyphs)
            {
                glyphCompletionItems.Add(CreateItem(glyph.Name, glyph.Image, context.Session));
            }

            return glyphCompletionItems;
        }
    }
}