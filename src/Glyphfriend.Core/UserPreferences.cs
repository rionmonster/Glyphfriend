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

        public static void ToggleLibrary(string library, bool isEnabled)
        {
            Settings.SetBoolean(Constants.UserSettingsLibrary, library, isEnabled);
            Constants.SupportedLibraries[library].Enabled = isEnabled;
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
            foreach (var library in Constants.SupportedLibraries.Keys)
            {
                // Check if this library exists, and if not include its key
                if (!Settings.PropertyExists(Constants.UserSettingsLibrary, library))
                {
                    // Store the default value
                    Settings.SetBoolean(Constants.UserSettingsLibrary, library, Constants.SupportedLibraries[library].Enabled);
                }
                else
                {
                    // Set the available value
                    Constants.SupportedLibraries[library].Enabled = Settings.GetBoolean(Constants.UserSettingsLibrary, library);
                }
            }
        }
    }
}
