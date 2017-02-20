using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Settings;

namespace Glyphfriend
{
    public class UserPreferences
    {
        public static WritableSettingsStore Settings { get; private set; }

        public static void Initialize(Package package)
        {
            var settingsManager = new ShellSettingsManager(package);
            Settings = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
            EnsureSettingsStoreExists();
            InitializeSupportedLibraries();
        }

        public static void ToggleLibrary(int libraryId, bool isEnabled)
        {
            // Resolve the library
            var library = Constants.SupportedLibraries[libraryId];
            Settings.SetBoolean(Constants.UserSettingsLibrary, library.Name, isEnabled);
            Constants.SupportedLibraries[libraryId].Enabled = isEnabled;
        }

        private static void EnsureSettingsStoreExists()
        {
            if (!Settings.CollectionExists(Constants.UserSettingsLibrary))
            {
                Settings.CreateCollection(Constants.UserSettingsLibrary);
            }
        }

        private static void InitializeSupportedLibraries()
        {
            // At this point we know the collection exists, so check support for each library available
            foreach (var libraryId in Constants.SupportedLibraries.Keys)
            {
                var library = Constants.SupportedLibraries[libraryId];
                // Check if this library exists, and if not include its key
                if (!Settings.PropertyExists(Constants.UserSettingsLibrary, library.Name))
                {
                    // Store the default value
                    Settings.SetBoolean(Constants.UserSettingsLibrary, library.Name, library.Enabled);
                }
                else
                {
                    library.Enabled = Settings.GetBoolean(Constants.UserSettingsLibrary, library.Name);
                }
            }
        }
    }
}
