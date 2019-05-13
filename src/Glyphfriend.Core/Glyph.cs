using ProtoBuf;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glyphfriend
{
    /// <summary>
    /// This class presents a single Glyph image and contains various informatio about it,
    /// its image contents, and a method to handle generating a usable ImageSource with
    /// the contents after it has been deserialized.
    /// </summary>
    [ProtoContract]
    internal class Glyph
    {
        [ProtoMember(1)]
        public string Name { get; private set; }

        [ProtoMember(2)]
        public string Library { get; private set; }

        [ProtoMember(3)]
        public byte[] ImageContent { get; private set; }

        [ProtoIgnore]
        public bool Enabled { get; set; }

        [ProtoIgnore]
        public ImageSource Image { get; set; }

        public Glyph()
        {
        }

        public Glyph(string name, string library, byte[] imageContent)
        {
            Name = name;
            Library = library;
            ImageContent = imageContent;
        }

        public void GenerateImage()
        {
            var image = new BitmapImage();
            using (var ms = new MemoryStream(ImageContent))
            {
                image.BeginInit();
                image.StreamSource = ms;

                // This is required for images to be loaded by Visual Studio on-demand
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            Image = image as ImageSource;
        }
    }

}