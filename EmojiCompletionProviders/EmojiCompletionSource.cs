using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace Glyphfriend.EmojiCompletionProviders
{
    class EmojiCompletionSource : ICompletionSource
    {
        private readonly ITextBuffer _buffer;
        private readonly List<Completion> _emojis;
        private bool _disposed = false;

        public EmojiCompletionSource(ITextBuffer buffer)
        {
            _buffer = buffer;
            _emojis = GlyphfriendPackage.Emojis
                                        .Select(e => EmojiCompletion(e.Key, e.Value))
                                        .ToList();
        }

        private static Completion EmojiCompletion(string emoji, ImageSource emojiImage)
        {
            // Map a completion object for each Emoji to the appropriate image
            var formattedEmoji = $":{emoji}:";
            return new Completion(formattedEmoji, formattedEmoji, formattedEmoji, emojiImage, formattedEmoji);
        }

        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            // If the completion source is already disposed, throw an exception
            if (_disposed)
            {
                throw new ObjectDisposedException("");
            }

            // See what the current buffer looks like
            ITextSnapshot snapshot = _buffer.CurrentSnapshot;
            var triggerPoint = (SnapshotPoint)session.GetTriggerPoint(snapshot);
            // If we can't resolve the current snapshot, ignore it
            if (triggerPoint == null)
            {
                return;
            }

            // Take a look at the current line to see if an Emoji exists    
            var line = triggerPoint.GetContainingLine();
            SnapshotPoint start = triggerPoint;

            // 
            var emojiFound = false;
            // Go through the line
            while (start > line.Start)
            {
                // Set our current character either 
                var character = start == snapshot.Length ? '\n' : start.GetChar();

                // We know this is the starting character for an Emoji, so flag it
                if (character == ':')
                {
                    emojiFound = true;
                    break;
                }

                // Check if the previous character is an Emoji character
                if (!IsEmojiCharacter((start - 1).GetChar()))
                {
                    break;
                }

                start -= 1;
            }

            // If no Emoji was found, ignore
            if (!emojiFound)
            {
                return;
            }

            // Define the Emojis that are applicable for the current span of Emoji characters
            var applicableTo = snapshot.CreateTrackingSpan(new SnapshotSpan(start, triggerPoint), SpanTrackingMode.EdgeInclusive);
            // Compare the current snapshot to the set of available Emojis and build a completion set
            // to display the available options
            completionSets.Add(new CompletionSet("All", "All", applicableTo, _emojis, Enumerable.Empty<Completion>()));
        }

        private bool IsEmojiCharacter(char character)
        {
            return Regex.IsMatch(Convert.ToString(character), @"[\w\-\+\:]");
        }

        public void Dispose()
        {
            _disposed = true;
        }
    }
}
