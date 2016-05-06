using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("MetroUI Font")]
    class MetroUIGlyphCompletionProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a Regular Expression check for matches from this library
        private static Regex _regex = new Regex(@"^metro-icons(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        // Store the Glyph folder related to this library
        private static string _lib = "MetroUI";

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename) && GlyphfriendPackage.Glyphs.ContainsKey(_lib))
            {
                if (GlyphfriendPackage.Glyphs[_lib].ContainsKey(entryName))
                {
                    return GlyphfriendPackage.Glyphs[_lib][entryName];
                }
            }
            return null;
        }
    }
}
