using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;

namespace Glyphfriend
{
    internal sealed class ToggleLibraryCommand
    {
        public static ToggleLibraryCommand Instance { get; private set; }
        
        private readonly Package _package;
        private IServiceProvider ServiceProvider => _package;

        public static void Initialize(Package package)
        {
            Instance = new ToggleLibraryCommand(package);
        }

        private ToggleLibraryCommand(Package package)
        {
            _package = package;

            var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                foreach (var library in Constants.Libraries.Keys)
                {
                    var command = CreateCommand(Constants.ToggleLibraryCommandSet, library);
                    commandService.AddCommand(command);
                }
            }
        }

        private MenuCommand CreateCommand(Guid commandSet, int commandId)
        {
            var library = Constants.Libraries[commandId];
            // Generate the command that will map this particular library to the appropriate button within the
            // .vsct file
            return new MenuCommand(ToggleLibrary, new CommandID(commandSet, commandId)) { Checked = library.Enabled };
        }

        private void ToggleLibrary(object sender, EventArgs e)
        {
            var command = (MenuCommand)sender;
            command.Checked = !command.Checked;
            GlyphfriendPreferences.ToggleLibrary(command.CommandID.ID, command.Checked);
        }
    }
}
