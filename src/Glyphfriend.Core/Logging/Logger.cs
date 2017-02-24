using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Glyphfriend
{
    /// <summary>
    /// A logger made specifically for Visual Studio extensions.
    /// </summary>
    public static class Logger
    {
        private static IVsOutputWindowPane _pane;
        private static IServiceProvider _provider;
        private static Guid _guid;
        private static string _name;
        private static object _syncRoot = new object();

        public static void Initialize(IServiceProvider provider, string name)
        {
            _provider = provider;
            _name = name;
        }

        public static void Initialize(IServiceProvider provider, string name, string version)
        {
            Initialize(provider, name);
        }

        public static void Log(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            try
            {
                if (EnsurePane())
                {
                    _pane.OutputStringThreadSafe($"{DateTime.Now}: {message} {Environment.NewLine}");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }

        public static void Log(Exception ex)
        {
            if (ex != null)
            {
                Log(ex.ToString());
            }
        }

        public static void Clear()
        {
            if (_pane != null)
            {
                _pane.Clear();
            }
        }

        public static void DeletePane()
        {
            if (_pane != null)
            {
                try
                {
                    IVsOutputWindow output = (IVsOutputWindow)_provider.GetService(typeof(SVsOutputWindow));
                    output.DeletePane(ref _guid);
                    _pane = null;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Write(ex);
                }
            }
        }

        private static bool EnsurePane()
        {
            if (_pane == null)
            {
                lock (_syncRoot)
                {
                    if (_pane == null)
                    {
                        _guid = Guid.NewGuid();
                        IVsOutputWindow output = (IVsOutputWindow)_provider.GetService(typeof(SVsOutputWindow));
                        output.CreatePane(ref _guid, _name, 1, 1);
                        output.GetPane(ref _guid, out _pane);
                    }
                }
            }

            return _pane != null;
        }
    }
}