using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;

using Task = System.Threading.Tasks.Task;

namespace Glyphfriend
{
    internal sealed class ToggleLibraryCommand
    {
        public static ToggleLibraryCommand Instance { get; private set; }

        public static async Task InitializeAsync(VSPackage package)
        {
            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ToggleLibraryCommand(commandService);
        }

        private ToggleLibraryCommand(OleMenuCommandService commandService)
        {
            foreach (var library in Constants.Libraries.Keys)
            {
                var command = CreateCommand(Constants.ToggleLibraryCommandSet, library);
                commandService.AddCommand(command);
            }
        }

        private MenuCommand CreateCommand(Guid commandSet, int commandId)
        {
            var library = Constants.Libraries[commandId];

            // Generate the command that will map this particular library to the appropriate button within the .vsct file
            return new OleMenuCommand(ToggleLibrary, new CommandID(commandSet, commandId)) { Checked = library.Enabled };
        }

        private void ToggleLibrary(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var command = (OleMenuCommand)sender;
            command.Checked = !command.Checked;
            GlyphfriendPreferences.ToggleLibrary(command.CommandID.ID, command.Checked);
        }
    }
}