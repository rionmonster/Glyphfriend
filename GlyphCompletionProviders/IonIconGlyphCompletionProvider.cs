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
    class IonIconCompletionEntryGlyphProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a default icon for any related classes that do not have glyphs
        private static BitmapFrame _defaultIcon = BitmapFrame.Create(new Uri("pack://application:,,,/Glyphfriend;component/Glyphs/IonIcon/ionic-logo.png", UriKind.RelativeOrAbsolute));

        // Define a pattern to handle matching any related IonIcon CSS files
        private static Regex _cssFileExpression = new Regex(@"^ionic(.*)?(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            if (sourceUri == null) { return null; }

            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            if (_cssFileExpression.IsMatch(filename))
            {
                try
                {
                    // Attempt to grab an icon for the current entry by trimming off the appropriate prefix for the entry (e.g. "ion-{entry}")
                    return BitmapFrame.Create(new Uri(String.Format("pack://application:,,,/Glyphfriend;component/Glyphs/IonIcon/{0}.png", entryName.Substring(4)), UriKind.RelativeOrAbsolute));
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
