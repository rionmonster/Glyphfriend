using Microsoft.CSS.Editor.Completion;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace Glyphfriend.GlyphCompletionProviders
{
    class BaseGlyphfriendProvider : ICssCompletionEntryGlyphProvider
    {
        /// <summary>
        /// A Regular Expression representing the file that is used to define the glyphs (e.g. @"^font(-?)awesome(-.*)?(\.min)?\.css$" for Font Awesome)
        /// </summary>
        protected Regex GlyphCSSDefinitionExpression { get; set; }
        /// <summary>
        /// The name of the Library (e.g. Font Awesome)
        /// </summary>
        protected string Library { get; set; }
        /// <summary>
        /// The default CSS class for this library (e.g. fa-default for Font Awesome)
        /// </summary>
        protected string DefaultIconClass { get; set; }

        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source is null or no glyphs have been loaded, ignore the request
            if (sourceUri == null || GlyphfriendPackage.Glyphs == null) { return null; }
            // Get the file path of the source being used
            var filename = Path.GetFileName(Convert.ToString(sourceUri));
            // Determine if the file matches this provider and we have the library loaded
            if (GlyphCSSDefinitionExpression.IsMatch(filename.Trim()) && GlyphfriendPackage.Glyphs.ContainsKey(Library))
            {
                // Since the library is loaded, determine if the Glyph exists
                if (entryName != null && GlyphfriendPackage.Glyphs[Library].ContainsKey(entryName))
                {
                    // It does, so serve it
                    return GlyphfriendPackage.Glyphs[Library][entryName];
                }
                // Otherwise attempt to see if a default icon is defined and loaded and use it
                if (DefaultIconClass != null && GlyphfriendPackage.Glyphs[Library].ContainsKey(DefaultIconClass))
                {
                    return GlyphfriendPackage.Glyphs[Library][DefaultIconClass];
                }
            }
            // Let Visual Studio handle this on its own
            return null;
        }
    }
}
