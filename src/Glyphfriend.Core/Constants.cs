using System;
using System.Collections.Generic;

namespace Glyphfriend
{
    class Constants
    {
        public const string HtmlFileLoadedContext = "21F5568E-A5DE-4821-AF39-F4F1049BB9CF";
        public static readonly Guid PackageGuid = new Guid("21F5568E-A5DE-4821-AF39-F4F1049BB9CF");

        public static readonly Dictionary<string, SupportedLibrary> SupportedLibraries = new Dictionary<string, SupportedLibrary>(){
            { "Bootstrap", new SupportedLibrary(){ Enabled = true, Prefix = "glyphicon-" } },
            { "Entypo", new SupportedLibrary(){ Enabled = false, Prefix = "icon-" } },
            { "Font Awesome", new SupportedLibrary(){ Enabled = false, Prefix = "fa-" } },
            { "Foundation", new SupportedLibrary(){ Enabled = false, Prefix = "fi-" } },
            { "Ionic", new SupportedLibrary(){ Enabled = false, Prefix = "ion-" } },
            { "Material Design", new SupportedLibrary(){ Enabled = false, Prefix = "mdi-" } },
            { "Metro UI", new SupportedLibrary(){ Enabled = false, Prefix = "mif-" } },
            { "Octicons", new SupportedLibrary(){ Enabled = false, Prefix = "octicon-" } }
        };
    }

    class SupportedLibrary
    {
        public bool Enabled { get; set; }
        public string Prefix { get; set; }
    }
}
