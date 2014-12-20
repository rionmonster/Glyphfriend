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
    class IcoMoonCompletionEntryGlyphProvider : ICssCompletionEntryGlyphProvider
    {
        // Define a default icon for any related classes that do not have glyphs
        private static BitmapFrame _defaultIcon = BitmapFrame.Create(new Uri("pack://application:,,,/Glyphfriend;component/Glyphs/IcoMoon/icomoon-logo.png", UriKind.RelativeOrAbsolute));

        // Define a pattern to handle matching any related IcoMoon CSS files (match any CSS files within an IcoMoon folder)
        private static Regex _cssFileExpression = new Regex(@"^(.*)IcoMoon/(.*)\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            if (sourceUri == null) { return null; }

            // Don't try and match an exact file (since they will vary), instead check the path for a match
            string filepath = sourceUri.AbsolutePath;
            if (_cssFileExpression.IsMatch(filepath))
            {
                try
                {
                    // Attempt to grab an icon for the current entry by trimming off the appropriate prefix for the entry (e.g. "icon-{entry}")
                    return BitmapFrame.Create(new Uri(String.Format("pack://application:,,,/Glyphfriend;component/Glyphs/IcoMoon/{0}.png", entryName.Substring(5).ToLower()), UriKind.RelativeOrAbsolute));
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
