using System;
using System.Collections.Generic;
using System.IO;
using Glyphfriend.Core;

namespace Glyphfriend
{
    public sealed class GlyphfriendPackage
    {
        internal static Dictionary<string, Dictionary<string, LazyImage>> Glyphs { get; } = new Dictionary<string, Dictionary<string, LazyImage>>();
        internal static Dictionary<string, LazyImage> Emojis { get; } = new Dictionary<string, LazyImage>();
        internal static bool AreGlyphsLoaded { get; private set; }
        internal static bool AreEmojisLoaded { get; private set; }
        internal static string Assembly => System.Reflection.Assembly.GetExecutingAssembly().Location;

        public static void LoadGlyphs()
        {
            // Construct a directory of Glyphs
            var glyphDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly),"Glyphfriend.Core","Glyphs"));
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach(var directory in glyphDirectory.EnumerateDirectories())
            {
                // Instantiate the dictionary with the name of the library
                Glyphs.Add(directory.Name, new Dictionary<string, LazyImage>());
                // Iterate through all of the glyphs in this dictionary
                foreach (var glyph in directory.EnumerateFiles("*.png", SearchOption.AllDirectories))
                {
                    try
                    {
                        // Created a lazily loaded glyph that points to the appropriate path
                        Glyphs[directory.Name].Add(Path.GetFileNameWithoutExtension(glyph.Name), new LazyImage { Path = new Uri(glyph.FullName, UriKind.RelativeOrAbsolute) });
                    }
                    catch (Exception)
                    {
                        // An error occurred when creating the glyph, ignore it (it will be handled in the provider)
                    }
                }
            }
            AreGlyphsLoaded = true;
        }

        public static void LoadEmojis()
        {
            // Construct a directory of Emojis
            var emojiDirectory = Path.Combine(Path.GetDirectoryName(Assembly),"Glyphfriend.Core","Emojis");
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach (var emoji in new DirectoryInfo(emojiDirectory).EnumerateFiles("*.png", SearchOption.AllDirectories))
            {
                try
                {
                    // Created a lazily loaded glyph that points to the appropriate path
                    Emojis.Add(Path.GetFileNameWithoutExtension(emoji.Name), new LazyImage { Path = new Uri(emoji.FullName, UriKind.RelativeOrAbsolute) });
                }
                catch (Exception)
                {
                    // An error occurred when creating the Emoji, ignore it (it will be handled in the provider)
                }
            }
            AreEmojisLoaded = true;
        }
    }
}
