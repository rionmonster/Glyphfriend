using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.CSS.Editor;
using Microsoft.Web.Editor.Intellisense;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    class FontAwesomeCompletionEntryGlyphProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a default icon for any related classes that do not have glyphs
        private static BitmapFrame _defaultIcon = BitmapFrame.Create(new Uri("pack://application:,,,/Glyphfriend;component/Glyphs/FontAwesome/font-awesome.png", UriKind.RelativeOrAbsolute));

        // Define a pattern to handle matching any related Font Awesome CSS files
        private static Regex _cssFileExpression = new Regex(@"^font-awesome(-.*)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            if (sourceUri == null) { return null; }

            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            if (_cssFileExpression.IsMatch(filename))
            {
                try
                {
                    // Attempt to grab an icon for the current entry
                    return BitmapFrame.Create(new Uri(String.Format("pack://application:,,,/Glyphfriend;component/Glyphs/FontAwesome/{0}.png", entryName.Substring(3)), UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    // If one was not available, serve the default icon
                    return _defaultIcon;
                }
            }

            return null;
        }
    }
}
