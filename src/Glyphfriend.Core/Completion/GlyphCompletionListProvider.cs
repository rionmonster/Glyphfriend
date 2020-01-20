using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Utilities;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using System.Collections.Generic;
using System.Linq;

namespace Glyphfriend
{
    [HtmlCompletionProvider(CompletionTypes.Values, "*", "class")]
    [ContentType("htmlx")]
    internal class GlyphCompletionListProvider : BaseHtmlCompletionListProvider
    {
        public override string CompletionType { get { return CompletionTypes.Values; } }

        public override IList<HtmlCompletion> GetEntries(HtmlCompletionContext context)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            VSPackage package = (VSPackage)EnsurePackageLoaded();
            if (package == null)
            {
                Logger.Log("Package failed to load properly!");
                return new List<HtmlCompletion>();
            }

            var glyphCompletionItems = new List<HtmlCompletion>();

           // Get the filtered set of enabled glyphs
           var enabledGlyphs = package.Glyphs.Where(g => g.Enabled);
            foreach (var glyph in enabledGlyphs)
            {
                glyphCompletionItems.Add(CreateItem(glyph.Name, glyph.Image, context.Session));
            }

            return glyphCompletionItems;
        }

        private IVsPackage EnsurePackageLoaded()
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var shell = (IVsShell)Package.GetGlobalService(typeof(SVsShell));
            if (shell.IsPackageLoaded(ref Constants.PackageGuid, out IVsPackage package) != VSConstants.S_OK)
            {
                ErrorHandler.Succeeded(shell.LoadPackage(ref Constants.PackageGuid, out package));
            }

            return package;
        }
    }
}