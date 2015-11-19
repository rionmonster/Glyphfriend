using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Glyphfriend.Helpers;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;

namespace Glyphfriend.EmojiCompletionProviders
{
    [ContentType(MarkdownContentTypeDefinition.MarkdownContentType)]
    [Order(After = "HTML Completion Source Provider")]
    [Name("Markdown Emoji")]
    [Export(typeof(ICompletionSourceProvider))]
    class EmojiCompletionProvider : ICompletionSourceProvider
    {
        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new EmojiCompletionSource(textBuffer);
        }
    }
}
