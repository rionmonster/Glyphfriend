using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Glyphfriend.Helpers;
using Microsoft.CSS.Editor.Completion;
using Microsoft.VisualStudio.Utilities;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Glyphfriend.GlyphCompletionProviders
{
    [Export(typeof(ICssCompletionEntryGlyphProvider))]
    [Name("Glyphfriend Bootstrap")]
    [Order(Before = "Default Bootstrap")]
    class BootstrapGlyphCompletionProvider : ICssCompletionEntryGlyphProvider
    {
        // Store each of the byte[] data for each font for future calls (so that the fonts only have to be generated once)
        private static Dictionary<string, BitmapFrame> _fontMappings;

        // Define a Regular Expression check for matches from this library
        private static Regex _regex = new Regex(@"^bootstrap(\.min)?\.css$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public BootstrapGlyphCompletionProvider()
        {
            // Initialize the Font Awesome icons for the renderer
            InitializeFonts(GlyphfriendPackage.CurrentSolutionPath);
            // Find any CSS files associated to the font-awesome library and build the necessary mappings
            ParseAndGenerateMappings(GlyphfriendPackage.CurrentSolutionPath);
        }

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source Uri for the image is null, ignore it
            if (sourceUri == null) { return null; }
            // Get the file path of the source being used
            string filename = Path.GetFileName(sourceUri.ToString()).Trim();
            // Determine if this matches our filename
            if (_regex.IsMatch(filename) && _fontMappings.ContainsKey(entryName))
            {
                return _fontMappings[entryName];
            }

            return null;
        }

        private void InitializeFonts(string directory)
        {
            // Instantiate all Font Awesome True Type fonts that are available (for the renderer)
            var fonts = new PrivateFontCollection();

            // Get the appropriate fonts that are present within this Project
            foreach (var fontFamily in Directory.GetFiles(directory, "glyphicons-halflings-regular.ttf", SearchOption.AllDirectories))
            {
                fonts.AddFontFile(fontFamily);
            }

            // Add each of the true type fonts to this renderer
            foreach (var trueTypeFont in fonts.Families)
            {
                HtmlRender.AddFontFamily(trueTypeFont);
            }
        }

        private void ParseAndGenerateMappings(string directory)
        {
            // Find the appropriate font-awesome CSS file
            var path = Directory.EnumerateFiles(directory, "*.css", SearchOption.AllDirectories).Where(f => Regex.IsMatch(f, @"bootstrap(\.min)?\.css")).FirstOrDefault();

            // Use the file if it was found
            if (!String.IsNullOrEmpty(path))
            {
                // Read in the CSS from the file
                var css = File.ReadAllText(path);

                // Read only the CSS Classes (only those with :before and beginning with .fa-)
                var classes = Regex.Matches(css, @"\.[-]?([_a-zA-Z][_a-zA-Z0-9-]*):before|[^\0-\177]*\\[0-9a-f]{1,6}(\r\n[ \n\r\t\f])?|\\[^\n\r\f0-9a-f]*")
                                   .Cast<Match>()
                                   .Where(m => m.Value.StartsWith(".glyphicon-") && m.Value.Contains("content:"))
                                   .Distinct();

                // Create a dictionary that maps class names to their respective unicode values
                Dictionary<string, string> mappings = new Dictionary<string, string>();

                // Using those classes, grab the content value that appears within each of them, which will map
                // the unicode values to the appropriate class
                foreach (var cssClass in classes)
                {
                    // Find the cooresponding code for each class
                    var unicode = Regex.Match(css, cssClass + "[^\"]*\"(.*)\"[^}]*}");
                    if (unicode != null && unicode.Groups.Count > 1)
                    {
                        // Grab the match and store it
                        mappings.Add(cssClass.Value.Replace(":before", "").TrimStart('.'), String.Format("&#x{0};", unicode.Groups[1].Value.TrimStart('\\')));
                    }
                }

                // Ensure the font mappings exists
                _fontMappings = _fontMappings ?? new Dictionary<string, BitmapFrame>();

                // Loop through the available mappings and generate a Glyph for each
                foreach (var mapping in mappings)
                {
                    // If the current mapping doesn't exist, add it
                    if (!_fontMappings.ContainsKey(mapping.Key))
                    {
                        // Generate an HTML Snippet to properly render the Glyph
                        var html = String.Format("<span style='font-family: \"Glyphicons Halflings\"; display:block;'>{0}</span>", mapping.Value);
                        // Build a glyphframe
                        var frame = ImageHelper.GenerateGlyph(html);
                        // Store it within the Dictionary
                        _fontMappings.Add(mapping.Key, frame);
                    }
                }
            }
        }
    }
}
