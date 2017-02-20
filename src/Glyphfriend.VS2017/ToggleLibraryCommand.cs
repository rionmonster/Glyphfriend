using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using System.Collections.Generic;

namespace Glyphfriend
{
    internal sealed class ToggleLibraryCommand
    {
        public static readonly Guid CommandSet = new Guid("faf962bd-d32b-4c73-a5d3-fcdf95277a21");
        public static ToggleLibraryCommand Instance { get; private set; }

        public const int CommandId = 0x0100;
        

        private readonly Package _package;
        private IServiceProvider ServiceProvider => _package;

        public static void Initialize(Package package)
        {
            Instance = new ToggleLibraryCommand(package);
        }

        private ToggleLibraryCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            _package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                foreach (var library in Constants.SupportedLibraries.Keys)
                {
                    var command = CreateCommand(CommandSet, library);
                    commandService.AddCommand(command);
                }
            }
        }

        private MenuCommand CreateCommand(Guid commandSet, int commandId)
        {
            // See what library this is
            var library = Constants.SupportedLibraries[commandId];
            // See if it is enabled by default
            var menuCommandID = new CommandID(commandSet, commandId);
            var command = new MenuCommand(ToggleLibrary, menuCommandID) { Checked = library.Enabled };
            return command;
        }

        private void ToggleLibrary(object sender, EventArgs e)
        {
            var command = (MenuCommand)sender;

            // See what library this is
            UserPreferences.ToggleLibrary(command.CommandID.ID, !command.Checked);

            // Update the command to reflect which are enabled
            command.Checked = !command.Checked;
        }
    }
}
