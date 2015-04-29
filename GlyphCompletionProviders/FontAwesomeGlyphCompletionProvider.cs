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
    [Name("Glyphfriend Font Awesome")]
    class FontAwesomeGlyphCompletionProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a Regular Expression check for matches from this library
        private static Regex _regex = new Regex(@"^font(-?)awesome(-.*)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename))
            {
                if (GlyphfriendPackage.Glyphs.ContainsKey(entryName))
                {
                    return GlyphfriendPackage.Glyphs[entryName];
                }
                return GlyphfriendPackage.Glyphs["fa-default"];
            }

            return null;
        }
    }
}
