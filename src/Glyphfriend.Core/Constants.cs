using System;
using System.Collections.Generic;

namespace Glyphfriend
{
    class Constants
    {
        public const string UserSettingsLibrary = "GlyphfriendLibraries";
        public const string HtmlFileLoadedContext = "21F5568E-A5DE-4821-AF39-F4F1049BB9CF";
        public static readonly Guid PackageGuid = new Guid("21F5568E-A5DE-4821-AF39-F4F1049BB9CF");

        public const int ToggleBootstrapCommand = 0x1101;
        public const int ToggleEntypoCommand = 0x1102;
        public const int ToggleFontAwesomeCommand = 0x1103;
        public const int ToggleFoundationCommand = 0x1104;
        public const int ToggleIonicCommand = 0x1105;
        public const int ToggleMaterialDesignCommand = 0x1106;
        public const int ToggleMetroUiCommand = 0x1107;
        public const int ToggleOcticonsCommand = 0x1108;

        public static readonly Dictionary<int, SupportedLibrary> SupportedLibraries = new Dictionary<int, SupportedLibrary>(){
            { ToggleBootstrapCommand, new SupportedLibrary("Bootstrap", "glyphicon-", true) },
            { ToggleEntypoCommand, new SupportedLibrary("Entypo", "icon-", false) },
            { ToggleFontAwesomeCommand, new SupportedLibrary("Font Awesome", "fa-", false) },
            { ToggleFoundationCommand, new SupportedLibrary("Foundation", "fi-", false) },
            { ToggleIonicCommand, new SupportedLibrary("Ionic", "ion-", false) },
            { ToggleMaterialDesignCommand, new SupportedLibrary("Material Design", "mdi-", false) },
            { ToggleMetroUiCommand, new SupportedLibrary("Metro UI", "mif-", false) },
            { ToggleOcticonsCommand, new SupportedLibrary("Octicons", "octicon-", false) },
        };
    }

    class SupportedLibrary
    {
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public string Prefix { get; set; }

        public SupportedLibrary(string name, string prefix, bool enabled)
        {
            Name = name;
            Prefix = prefix;
            Enabled = enabled;
        }
    }
}
