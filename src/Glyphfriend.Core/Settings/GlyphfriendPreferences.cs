using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;
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
        private static VSPackage _package;

        public static void Initialize(Package package)
        {
            _package = (VSPackage)package;
            Settings = new ShellSettingsManager(package).GetWritableSettingsStore(SettingsScope.UserSettings);
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
            _package.Glyphs.Where(g => g.Library == library.Name)
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
    }
}