using System;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Task = System.Threading.Tasks.Task;

namespace Glyphfriend
{
    internal static class Logger
    {
        private static string _name;
        private static Guid _guid = new Guid();
        private static IVsOutputWindowPane _pane;
        private static IVsOutputWindow _output;

        public static void Initialize(IServiceProvider provider, string name)
        {
            _output = (IVsOutputWindow)provider.GetService(typeof(SVsOutputWindow));
            _name = name;
        }

        public static async Task InitializeAsync(VSPackage package, string name)
        {
            _output = await package.GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;
            _name = name;
        }

        public static void Log(object message)
        {
            try
            {
                if (EnsurePane())
                {
                    _pane.OutputString($"{DateTime.Now}: {message} {Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }

        public static void DeletePane()
        {
            if (_output != null)
            {
                _output.DeletePane(_guid);
            }
        }

        private static bool EnsurePane()
        {
            if (_pane == null && _output != null)
            {
                ThreadHelper.Generic.BeginInvoke(() =>
                {
                    _output.CreatePane(ref _guid, _name, 1, 1);
                    _output.GetPane(ref _guid, out _pane);
                });
            }

            return _pane != null;
        }
    }
}
