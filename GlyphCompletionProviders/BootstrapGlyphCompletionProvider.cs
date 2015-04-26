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
    [Name("Glyphfriend Bootstrap")]
    [Order(Before = "Default Bootstrap")]
    class BootstrapGlyphCompletionProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a Regular Expression check for matches from this library
        private static Regex _regex = new Regex(@"^bootstrap(-.*)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename))
            {
                try
                {
                    // Attempt to grab an icon for the current entry
                    return BitmapFrame.Create(new Uri(String.Format("pack://application:,,,/Glyphfriend;component/Glyphs/Bootstrap/{0}.png", entryName.Substring(10)), UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    // If one was not available, serve the default icon (null as the default VS intellisense will take over)
                    return null;
                }
            }
            
            return null;
        }
    }
}
