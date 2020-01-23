using Microsoft;
using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell.Settings;
using Microsoft.VisualStudio.Threading;
using ProtoBuf;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Glyphfriend
{
    /// <summary>
    /// This class handles managing user preferences within the extension, specifically
    /// the enabling/disabling of certain libraries which in turn affects which glyphs are
    /// served to the user
    /// </summary>
    public static class GlyphfriendPreferences
    {
        public static WritableSettingsStore Settings { get; private set; }
        public static List<Glyph> Glyphs { get; private set; }

        private static ShellSettingsManager SettingsManagerInstance { get; set; }
        private static readonly AsyncLazy<ShellSettingsManager> SettingsManager = new AsyncLazy<ShellSettingsManager>(GetSettingsManagerAsync, ThreadHelper.JoinableTaskFactory);

        public static async System.Threading.Tasks.Task InitializeAsync()
        {
            SettingsManagerInstance = await SettingsManager.GetValueAsync();

            Settings = SettingsManagerInstance.GetWritableSettingsStore(SettingsScope.UserSettings);
            Glyphs = LoadSupportedGlyphs();
            EnsureSettingsStoreExists();
            InitializeSupportedLibraries();
        }

        public static void ToggleLibrary(int libraryId, bool isEnabled)
        {
            // Resolve the library
            var library = Constants.Libraries[libraryId];

            // Update settings
            Settings.SetBoolean(Constants.UserSettingsLibrary, library.Name, isEnabled);

            // Update menus
            Constants.Libraries[libraryId].Enabled = isEnabled;

            // Update glyphs
            Glyphs.Where(g => g.Library == library.Name)
                  .Select(g => { g.Enabled = isEnabled; return g; })
                  .ToList();

        }

        private static void EnsureSettingsStoreExists()
        {
            if (!Settings.CollectionExists(Constants.UserSettingsLibrary))
            {
                Settings.CreateCollection(Constants.UserSettingsLibrary);
                Logger.Log($"Glyphfriend preferences created!");
            }
            else
            {
                Logger.Log($"Glyphfriend preferences loaded!");
            }
        }

        private static void InitializeSupportedLibraries()
        {
            // At this point we know the collection exists, so check support for each library available
            foreach (var libraryId in Constants.Libraries.Keys)
            {
                var library = Constants.Libraries[libraryId];

                // Check if this library exists, and if not, set its default value from the extension
                if (!Settings.PropertyExists(Constants.UserSettingsLibrary, library.Name))
                {
                    Settings.SetBoolean(Constants.UserSettingsLibrary, library.Name, library.Enabled);
                }
                else
                {
                    library.Enabled = Settings.GetBoolean(Constants.UserSettingsLibrary, library.Name);
                }

                Logger.Log($"Library '{library.Name}' is {(library.Enabled ? "enabled" : "disabled")}.");
                ToggleLibrary(libraryId, library.Enabled);
            }
        }

        private static List<Glyph> LoadSupportedGlyphs()
        {
            var binaryPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "glyphs.bin");
            using (var fs = File.Open(binaryPath, FileMode.Open))
            {
                var glyphs = Serializer.Deserialize<List<Glyph>>(fs);
                glyphs.ForEach(g => g.GenerateImage());

                Logger.Log($"{glyphs.Count} glyphs found.");
                return glyphs;
            }
        }

        private static async System.Threading.Tasks.Task<ShellSettingsManager> GetSettingsManagerAsync()
        {
#pragma warning disable VSTHRD010
            // False-positive in Threading Analyzers. Bug tracked here https://github.com/Microsoft/vs-threading/issues/230
            var svc = await AsyncServiceProvider.GlobalProvider.GetServiceAsync(typeof(SVsSettingsManager)) as IVsSettingsManager;
#pragma warning restore VSTHRD010

            Assumes.Present(svc);
            
            return new ShellSettingsManager(svc);
        }
    }
}