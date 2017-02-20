using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Glyphfriend
{
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [ProvideMenuResource("GlyphfriendHtmlMenu.ctmenu", 1)]
    [Guid(ToggleLibraryCommandPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(PackageGuidString)]
    [ProvideUIContextRule(PackageGuidString,
        name: "HTML File Loaded",
        expression: "HtmlConfig",
        termNames: new[] { "HtmlConfig" },
        termValues: new[] { "ActiveEditorContentType:htmlx" })]
    public sealed class ToggleLibraryCommandPackage : Package
    {
        public const string PackageGuidString = "64130043-0eb2-4525-92bc-f74c6c2039d9";

        protected override void Initialize()
        {
            ToggleLibraryCommand.Initialize(this);
            base.Initialize();
        }
    }
}
