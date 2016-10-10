using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Glyphfriend.Helpers.Markdown;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

namespace Glyphfriend.EmojiCompletionProviders
{
    [ContentType(MarkdownContentTypeDefinition.MarkdownContentType)]
    [Name("Markdown Emoji")]
    [Export(typeof(ICompletionSourceProvider))]
    internal class EmojiCompletionProvider : ICompletionSourceProvider
    {
        [Import]
        private ITextStructureNavigatorSelectorService _textStructureNavigatorSelector;

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            // Build the Emoji completion handler for the current text buffer
            return new EmojiCompletionSource(_textStructureNavigatorSelector.GetTextStructureNavigator(textBuffer));
        }
    }
}
