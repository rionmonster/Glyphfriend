using Microsoft.CSS.Editor.Completion;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Glyphfriend.Core;

namespace Glyphfriend.GlyphCompletionProviders
{
    internal class BaseGlyphfriendProvider : ICssCompletionEntryGlyphProvider
    {
        /// <summary>
        /// The default CSS class for this library (e.g. fa-default for Font Awesome)
        /// </summary>
        protected string DefaultIconClass { get; set; }
        /// <summary>
        /// A Regular Expression representing the file that is used to define the glyphs (e.g. @"^font(-?)awesome(-.*)?(\.min)?\.css$" for Font Awesome)
        /// </summary>
        protected Regex GlyphCssDefinitionExpression { get; set; }
        /// <summary>
        /// The name of the Library (e.g. Font Awesome)
        /// </summary>
        protected string Library { get; set; }

        /// <summary>
        /// Attempts to load a glyph based on its current name and where it was defined. First Glyphfriend will 
        /// check and attempt to load the glyph if it exists, otherwise it will let Visual Studio handle it
        /// </summary>
        /// <param name="entryName">The name of the glyph (e.g. fa-beer, glyphicon-alert, etc.)</param>
        /// <param name="sourceUri">The URL that cooresponds to the location of the glyph (e.g. the font-awesome.css file for fa-beer, etc.)</param>
        /// <param name="nameType"></param>
        /// <returns></returns>
        public ImageSource GetCompletionGlyph(string entryName, Uri sourceUri, CssNameType nameType)
        {
            // If the source is null or no glyphs have been loaded, ignore the request
            if (sourceUri == null || GlyphfriendPackage.Glyphs == null) { return null; }
            // Get the file path of the source being used
            var filename = Path.GetFileName(Convert.ToString(sourceUri));
            // Determine if the file matches this provider and we have the library loaded
            if (GlyphCssDefinitionExpression.IsMatch(filename.Trim()) && GlyphfriendPackage.Glyphs.ContainsKey(Library))
            {
                // Since the library is loaded, determine if the Glyph exists
                if (entryName != null && GlyphfriendPackage.Glyphs[Library].ContainsKey(entryName))
                {
                    // It does, so lazily resolve the image
                    return GlyphfriendPackage.Glyphs[Library][entryName]?.Image;
                }
                // Otherwise attempt to see if a default icon is defined and loaded and use it
                if (DefaultIconClass != null && GlyphfriendPackage.Glyphs[Library].ContainsKey(DefaultIconClass))
                {
                    return GlyphfriendPackage.Glyphs[Library][DefaultIconClass]?.Image;
                }
            }
            // Let Visual Studio handle this on its own
            return null;
        }
    }
}
