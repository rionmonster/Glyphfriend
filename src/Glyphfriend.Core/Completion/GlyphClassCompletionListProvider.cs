using Microsoft.Html.Editor.Completion;
using Microsoft.Html.Editor.Completion.Def;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;

namespace Glyphfriend
{
    [HtmlCompletionProvider(CompletionTypes.Values, "*", "class")]
    [ContentType("htmlx")]
    class GlyphClassCompletionListProvider : BaseClassCompletionProvider
    {
        public override string CompletionType
        {
            get { return CompletionTypes.Values; }
        }

        public override IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            var completionItems = new List<HtmlCompletion>();
            foreach (var glyph in VSPackage.Glyphs)
            {
                completionItems.Add(CreateItem(glyph.Key, glyph.Value, context.Session));
            }
            return completionItems;
        }
    }
}