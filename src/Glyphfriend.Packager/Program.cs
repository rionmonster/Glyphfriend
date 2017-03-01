using ProtoBuf;
using System.Collections.Generic;
using System.IO;

namespace Glyphfriend.Packager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GenerateBinaryGlyphsFile();
        }

        private static void GenerateBinaryGlyphsFile()
        {
            var glyphs = ConvertGlyphsToList();
            BinarySerializeGlyphsToFile(glyphs);
        }

        private static IList<Glyph> ConvertGlyphsToList()
        {
            var glyphs = new List<Glyph>();
            var glyphDirectory = new DirectoryInfo("../../Glyphs");
            foreach (var image in glyphDirectory.EnumerateFiles("*.png", SearchOption.AllDirectories))
            {
                var imageContent = StreamHelpers.ReadFully(new FileStream(image.FullName, FileMode.Open));
                var glyph = new Glyph(Path.GetFileNameWithoutExtension(image.Name), image.Directory.Name, imageContent);

                glyphs.Add(glyph);
            }
            return glyphs;
        }

        private static void BinarySerializeGlyphsToFile(IList<Glyph> glyphs)
        {
            using (var ms = new FileStream("../../Binary/glyphs.bin", FileMode.Create))
            {
                Serializer.Serialize(ms, glyphs);
                ms.Flush();
            }
        }
    }
}