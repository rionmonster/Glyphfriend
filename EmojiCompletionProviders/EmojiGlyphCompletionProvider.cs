using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using Glyphfriend.Helpers;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Text.Classification;

namespace Glyphfriend.EmojiCompletionProviders
{
    [ContentType(MarkdownContentTypeDefinition.MarkdownContentType)]
    [Name("Markdown Emoji")]
    [Export(typeof(ICompletionSourceProvider))]
    class EmojiCompletionProvider : ICompletionSourceProvider
    {
        [Import]
        public ITextStructureNavigatorSelectorService TextStructureNavigatorSelector = null;

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            // Build the Emoji completion handler for the current text buffer
            return new EmojiCompletionSource(textBuffer, TextStructureNavigatorSelector.GetTextStructureNavigator(textBuffer));
        }
    }
}
