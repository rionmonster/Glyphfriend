using Microsoft.VisualStudio.Shell;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace Glyphfriend
{
    /// <summary>
    /// This is the core package and entry point for Glyphfriend and it handles
    /// loading all of the underlying components of the extension. It is not
    /// intialized until a valid HTMLX file (i.e. HTML, CSHTML, etc.) is opened.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [ProvideAutoLoad(Constants.HtmlFileLoadedContextString, PackageAutoLoadFlags.BackgroundLoad)]
    [Guid(Constants.PackageGuidString)]
    [ProvideMenuResource("GlyphfriendMenu.ctmenu", 1)]
    [ProvideUIContextRule(Constants.HtmlFileLoadedContextString,
        name: "HTML File Loaded",
        expression: "HtmlConfig",
        termNames: new[] { "HtmlConfig" },
        termValues: new[] { "ActiveEditorContentType:htmlx" })]
    public sealed class VSPackage : AsyncPackage
    {
        internal List<Glyph> Glyphs { get; private set; }
        internal static string AssemblyLocation => Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await Logger.InitializeAsync(this, "Glyphfriend");
            DeserializeGlyphsFromBinary();

            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            
            await GlyphfriendPreferences.InitializeAsync(this);
            await ToggleLibraryCommand.InitializeAsync(this);
        }

        private void DeserializeGlyphsFromBinary()
        {
            Glyphs = DeserializeBinaryGlyphs();
            Logger.Log($"{Glyphs.Count} supported glyphs found.");
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