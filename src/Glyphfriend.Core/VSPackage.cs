using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Glyphfriend
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideAutoLoad(Constants.HtmlFileLoadedContext)]
    [ProvideUIContextRule(Constants.HtmlFileLoadedContext,
        name: "HTML File Loaded",
        expression: "HtmlConfig",
        termNames: new[] { "HtmlConfig" },
        termValues: new[] { "ActiveEditorContentType:htmlx" })]
    public sealed class VSPackage : Package
    {
        internal Dictionary<string, ImageSource> Glyphs { get; private set; }
        internal static string AssemblyLocation => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        protected override void Initialize()
        {
            UserPreferences.Initialize(this);
            DeserializeGlyphsFromBinary();
        }

        private void DeserializeGlyphsFromBinary()
        {
            var binaryGlyphDictionary = DeserializeBinaryGlyphs();
            Glyphs = ConvertBinaryGlyphDictionaryToGlyphDictionary(binaryGlyphDictionary);
        }

        private Dictionary<string, byte[]> DeserializeBinaryGlyphs()
        {
            var binaryPath = Path.Combine(AssemblyLocation, "glyphs.bin");
            using (var fs = File.Open(binaryPath, FileMode.Open))
            {
                return Serializer.Deserialize<Dictionary<string, byte[]>>(fs);
            }
        }

        private Dictionary<string, ImageSource> ConvertBinaryGlyphDictionaryToGlyphDictionary(Dictionary<string, byte[]> dictionary)
        {
            return dictionary.ToDictionary(k => k.Key, v => BytesToImage(v.Value));
        }

        private ImageSource BytesToImage(byte[] imageData)
        {
            var image = new BitmapImage();
            using (var ms = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.StreamSource = ms;
                // This is required for images to be loaded by Visual Studio on-demand
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image as ImageSource;
        }
    }
}
