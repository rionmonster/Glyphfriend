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
        public const int ToggleBootstrapCommand = 0x1101;
        public const int ToggleEntypoCommand = 0x1102;
        public const int ToggleFontAwesomeCommand = 0x1103;
        public const int ToggleFoundationCommand = 0x1104;
        public const int ToggleIonicCommand = 0x1105;
        public const int ToggleMaterialDesignCommand = 0x1106;
        public const int ToggleMetroUiCommand = 0x1107;
        public const int ToggleOcticonsCommand = 0x1108;

        private Dictionary<int, string> _libraries = new Dictionary<int, string>()
        {
            { ToggleBootstrapCommand, "Bootstrap" },
            { ToggleEntypoCommand, "Entypo" },
            { ToggleFontAwesomeCommand, "Font Awesome" },
            { ToggleFoundationCommand, "Foundation" },
            { ToggleIonicCommand, "Ionic" },
            { ToggleMaterialDesignCommand, "Material Design" },
            { ToggleMetroUiCommand, "Metro UI" },
            { ToggleOcticonsCommand, "Octicons" }
        };
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
                foreach (var library in _libraries.Keys)
                {
                    var command = CreateCommand(CommandSet, library);
                    commandService.AddCommand(command);
                }
            }
        }

        private MenuCommand CreateCommand(Guid commandSet, int commandId)
        {
            // See what library this is
            var libraryName = _libraries[commandId];
            // See if it is enabled by default
            var menuCommandID = new CommandID(commandSet, commandId);
            var command = new MenuCommand(ToggleLibrary, menuCommandID) { Checked = Constants.SupportedLibraries[libraryName].Enabled };
            return command;
        }

        private void ToggleLibrary(object sender, EventArgs e)
        {
            var command = (MenuCommand)sender;
            // Update the supported libraries

            // See what library this is
            var libraryName = _libraries[command.CommandID.ID];
            Constants.SupportedLibraries[libraryName].Enabled = !command.Checked;

            // Update the command to reflect which are enabled
            command.Checked = !command.Checked;
        }
    }
}
