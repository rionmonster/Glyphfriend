#if (VS2019)
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
#else
using Microsoft.Html.Editor.Completion;
using Microsoft.Html.Editor.Completion.Def;
#endif

using Microsoft.VisualStudio.Language.Intellisense;
using System.Collections.Generic;
using System.Windows.Media;

namespace Glyphfriend
{
    internal abstract class BaseHtmlCompletionListProvider : IHtmlCompletionListProvider
    {
        public abstract string CompletionType { get; }

        public abstract IList<HtmlCompletion> GetEntries(HtmlCompletionContext context);

        protected HtmlCompletion CreateItem(string name, ImageSource icon, ICompletionSession session)
        {
            return new HtmlCompletion(name, name, name, icon, null, session);
        }
    }
}