using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.WebTools.Languages.Html.Editor.Completion;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Def;
using Microsoft.WebTools.Languages.Html.Editor.Completion.Html;
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
            return new HtmlCompletion(name, name, string.Empty, icon, HtmlIconAutomationText.AttributeIconText, session);
        }
    }
}