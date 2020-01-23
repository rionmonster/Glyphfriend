using Microsoft.VisualStudio.Shell;
using System;
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
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            await Logger.InitializeAsync(this, "Glyphfriend");
            await GlyphfriendPreferences.InitializeAsync();
            await ToggleLibraryCommand.InitializeAsync(this);
        }
    }
}