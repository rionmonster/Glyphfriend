using Microsoft.Html.Editor.Completion;
using Microsoft.Html.Editor.Completion.Def;
using Microsoft.VisualStudio.Language.Intellisense;
using System.Collections.Generic;
using System.Windows.Media;

namespace Glyphfriend
{
    abstract class BaseHtmlCompletionListProvider : IHtmlCompletionListProvider
    {
        public abstract string CompletionType { get; }

        public abstract IList<HtmlCompletion> GetEntries(HtmlCompletionContext context);

        protected HtmlCompletion CreateItem(string name, ImageSource icon, ICompletionSession session)
        {
            return new HtmlCompletion(name, name, name, icon, null, session);
        }
    }
}
