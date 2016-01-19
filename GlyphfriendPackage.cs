using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glyphfriend
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    public sealed class GlyphfriendPackage : Package
    {
        private static Dictionary<string, Dictionary<string,ImageSource>> _glyphs;
        internal static Dictionary<string, Dictionary<string, ImageSource>> Glyphs
        {
            get
            {
                return _glyphs;
            }
        }

        private static Dictionary<string, ImageSource> _emojis;
        internal static Dictionary<string, ImageSource> Emojis
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

        public static void LoadGlyphs()
        {
            // Instantiate the Glyph Dictionary
            _glyphs = _glyphs ?? new Dictionary<string, Dictionary<string,ImageSource>>();
            // Construct a directory of Glyphs
            var glyphDirectory = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly), "Glyphs"));
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach(var directory in glyphDirectory.EnumerateDirectories())
            {
                // Instantiate the dictionary with the name of the library
                _glyphs.Add(directory.Name, new Dictionary<string, ImageSource>());
                // Iterate through all of the glyphs in this dictionary
                foreach (var glyph in directory.EnumerateFiles("*.png", SearchOption.AllDirectories))
                {
                    try
                    {
                        // Generate the Glyph
                        var glyphBitmap = BitmapFrame.Create(new Uri(glyph.FullName, UriKind.RelativeOrAbsolute));
                        // Add it to the collection (under the specific library)
                        _glyphs[directory.Name].Add(Path.GetFileNameWithoutExtension(glyph.Name), glyphBitmap);
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
            _emojis = _emojis ?? new Dictionary<string, ImageSource>();

            // Construct a directory of Emojis
            var emojiDirectory = Path.Combine(Path.GetDirectoryName(Assembly), "Emojis");
            // Loop through all of the directories and map the glyphs to their appropriate libraries
            foreach (var emoji in new DirectoryInfo(emojiDirectory).EnumerateFiles("*.png", SearchOption.AllDirectories))
            {
                try
                {
                    // Generate the Glyph
                    var emojiBitmap = BitmapFrame.Create(new Uri(emoji.FullName, UriKind.RelativeOrAbsolute));
                    _emojis.Add(Path.GetFileNameWithoutExtension(emoji.Name), emojiBitmap);
                }
                catch (Exception)
                {
                    // An error occurred when creating the Emoji, ignore it (it will be handled in the provider)
                }
            }
        }
    }
}
