using System;
using System.Collections.Generic;

namespace Glyphfriend
{
    internal class Constants
    {
        /// <summary>
        /// These settings are specific to Glyphfriend itself and its associated commands
        /// </summary>
        public const string UserSettingsLibrary = "GlyphfriendLibrarySettings";
        public const string PackageGuidString = "21F5568E-A5DE-4821-AF39-F4F1049BB9CF";
        public const string HtmlFileLoadedContextString = "21F5568E-A5DE-4821-AF39-F4F1049BB9CF";
        public const string ToggleLibraryCommandSetString = "FAF962BD-D32B-4C73-A5D3-FCDF95277A21";

        public static Guid PackageGuid = new Guid("21F5568E-A5DE-4821-AF39-F4F1049BB9CF");
        public static Guid HtmlFileLoadedContext = new Guid("21F5568E-A5DE-4821-AF39-F4F1049BB9CF");
        public static Guid ToggleLibraryCommandSet = new Guid("FAF962BD-D32B-4C73-A5D3-FCDF95277A21");

        /// <summary>
        /// These commands are associated with the items found within the VSCT files and handles configuring
        /// the menu items that appear. Thus any new libraries will need to have key defined here and
        /// a button set within the VSCT file as well.
        /// </summary>
        public const int ToggleLibraryCommandId = 0x0100;

        public const int ToggleBootstrapCommand = 0x1101;
        public const int ToggleEntypoCommand = 0x1102;
        public const int ToggleFontAwesomeCommand = 0x1103;
        public const int ToggleFoundationCommand = 0x1104;
        public const int ToggleIonicCommand = 0x1105;
        public const int ToggleMaterialDesignCommand = 0x1106;
        public const int ToggleMetroUiCommand = 0x1107;
        public const int ToggleOcticonsCommand = 0x1108;
        public const int ToggleIcoMoonCommand = 0x1109;
        public const int ToggleRpgAwesomeCommand = 0x1110;


        /// <summary>
        /// These are all of the supported libraries in Glyphfriend along with their associated default
        /// values (i.e. Bootstrap and Font Awesome are enabled by default).
        /// </summary>
        public static readonly Dictionary<int, GlyphLibrary> Libraries = new Dictionary<int, GlyphLibrary>(){
            { ToggleBootstrapCommand, new GlyphLibrary("Bootstrap",  true) },
            { ToggleEntypoCommand, new GlyphLibrary("Entypo",  false) },
            { ToggleFontAwesomeCommand, new GlyphLibrary("Font Awesome",  true) },
            { ToggleFoundationCommand, new GlyphLibrary("Foundation",  false) },
            { ToggleIonicCommand, new GlyphLibrary("Ionic", false) },
            { ToggleMaterialDesignCommand, new GlyphLibrary("Material Design", false) },
            { ToggleMetroUiCommand, new GlyphLibrary("Metro UI",  false) },
            { ToggleOcticonsCommand, new GlyphLibrary("Octicons", false) },
            { ToggleIcoMoonCommand, new GlyphLibrary("IcoMoon", false) },
            { ToggleRpgAwesomeCommand, new GlyphLibrary("RPG Awesome", false) }

        };
    }
}