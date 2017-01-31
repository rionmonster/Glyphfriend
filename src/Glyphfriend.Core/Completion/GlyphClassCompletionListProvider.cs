using Glyphfriend.Extensions;
using Microsoft.Html.Editor.Completion;
using Microsoft.Html.Editor.Completion.Def;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Utilities;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Glyphfriend
{
    [HtmlCompletionProvider(CompletionTypes.Values, "*", "class")]
    [ContentType("htmlx")]
    class GlyphClassCompletionListProvider : BaseClassCompletionProvider
    {
        [Import]
        protected SVsServiceProvider GlobalServiceProvider { get;  private set; }

        private bool _glyphsLoaded;

        public override string CompletionType => CompletionTypes.Values;

        public override IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            if (!_glyphsLoaded)
            {
                LoadGlyphs();
            }

            var completionItems = new List<HtmlCompletion>();
            foreach (var glyph in VSPackage.Glyphs)
            {
                completionItems.Add(CreateItem(glyph.Key, glyph.Value, context.Session));
            }
            return completionItems;
        }

        /// <summary>
        /// This method only exists to resolve possible race conditions when the package itself
        /// was not loaded prior to an autocompletion call.
        /// </summary>
        private void LoadGlyphs()
        {
            var package = GlobalServiceProvider.GetShell().LoadPackage<VSPackage>();
            _glyphsLoaded = package != null;
        }
    }
}