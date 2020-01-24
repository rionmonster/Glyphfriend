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
        private static List<Glyph> Glyphs => Preferences.Glyphs;

        public override string CompletionType { get { return CompletionTypes.Values; } }

        public override IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            if (Glyphs == null)
            {
                return new List<HtmlCompletion>();
            }

            // Get the filtered set of enabled glyphs
            return Glyphs.Where(g => g.Enabled)
                         .Select(g => CreateItem(g.Name, g.Image, context.Session))
                         .ToList();
        }
    }
}