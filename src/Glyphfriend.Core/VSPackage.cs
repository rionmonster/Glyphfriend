using Microsoft.VisualStudio.Shell;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;

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
        internal List<Glyph> Glyphs { get; private set; }
        internal static string AssemblyLocation => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        protected override void Initialize()
        {
            DeserializeGlyphsFromBinary();
            GlyphfriendPreferences.Initialize(this);
        }

        private void DeserializeGlyphsFromBinary()
        {
            Glyphs = DeserializeBinaryGlyphs();
        }

        private List<Glyph> DeserializeBinaryGlyphs()
        {
            var binaryPath = Path.Combine(AssemblyLocation, "glyphs.bin");
            using (var fs = File.Open(binaryPath, FileMode.Open))
            {
                var glyphs = Serializer.Deserialize<List<Glyph>>(fs);
                glyphs.ForEach(g => g.GenerateImage());
                return glyphs;
            }
        }
    }
}
