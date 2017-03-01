namespace Glyphfriend
{
    /// <summary>
    /// This class presents a single Glyph library and simply stores the name of the library and
    /// if it is enabled or not.
    /// </summary>
    internal class GlyphLibrary
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }

        public GlyphLibrary(string name, bool enabled)
        {
            Name = name;
            Enabled = enabled;
        }
    }
}