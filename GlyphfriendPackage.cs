using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Glyphfriend.Helpers;

namespace Glyphfriend
{
    public sealed class GlyphfriendPackage
    {
        private static Dictionary<string, Dictionary<string,LazyImage>> _glyphs;
        internal static Dictionary<string, Dictionary<string, LazyImage>> Glyphs
        {
            get
            {
                return _glyphs;
            }
        }

        private static Dictionary<string, LazyImage> _emojis;
        internal static Dictionary<string, LazyImage> Emojis
        {
            get
            {
                return _emojis;
            }
        }

        internal static string Assembly
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
            }
        }

        internal static bool AreGlyphsLoaded { get; set; } = false;
        internal static bool AreEmojisLoaded { get; set; } = false;

        public static void LoadGlyphs()
        {
            // Instantiate the Glyph Dictionary
            _glyphs = _glyphs ?? new Dictionary<string, Dictionary<string, LazyImage>>();
            // Construct a directory of Glyphs
            var glyphDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly), "Glyphs"));
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach(var directory in glyphDirectory.EnumerateDirectories())
            {
                // Instantiate the dictionary with the name of the library
                _glyphs.Add(directory.Name, new Dictionary<string, LazyImage>());
                // Iterate through all of the glyphs in this dictionary
                foreach (var glyph in directory.EnumerateFiles("*.png", SearchOption.AllDirectories))
                {
                    try
                    {
                        // Created a lazily loaded glyph that points to the appropriate path
                        _glyphs[directory.Name].Add(Path.GetFileNameWithoutExtension(glyph.Name), new LazyImage { Path = new Uri(glyph.FullName, UriKind.RelativeOrAbsolute) });
                    }
                    catch (Exception)
                    {
                        // An error occurred when creating the glyph, ignore it (it will be handled in the provider)
                    }
                }
            }
        }

        public static void LoadEmojis()
        {
            // Instantiate the Glyph Dictionary
            _emojis = _emojis ?? new Dictionary<string, LazyImage>();

            // Construct a directory of Emojis
            var emojiDirectory = Path.Combine(Path.GetDirectoryName(Assembly), "Emojis");
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach (var emoji in new DirectoryInfo(emojiDirectory).EnumerateFiles("*.png", SearchOption.AllDirectories))
            {
                try
                {
                    // Created a lazily loaded glyph that points to the appropriate path
                    _emojis.Add(Path.GetFileNameWithoutExtension(emoji.Name), new LazyImage { Path = new Uri(emoji.FullName, UriKind.RelativeOrAbsolute) });
                }
                catch (Exception)
                {
                    // An error occurred when creating the Emoji, ignore it (it will be handled in the provider)
                }
            }
        }
    }
}
