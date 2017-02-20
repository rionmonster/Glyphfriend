using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace Glyphfriend
{
    public static class ServiceProviderExtensions
    {
        public static TServiceInterface GetService<TServiceClass, TServiceInterface>(this IServiceProvider serviceProvider)
        {
            return (TServiceInterface)serviceProvider.GetService(typeof(TServiceClass));
        }

        public static IVsShell GetShell(this SVsServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<SVsShell, IVsShell>();
        }

        public static T LoadPackage<T>(this IVsShell shell) where T : Package
        {
            Guid guid = typeof(T).GUID;
            IVsPackage package;
            ErrorHandler.ThrowOnFailure(shell.LoadPackage(ref guid, out package));
            return (T)package;
        }
    }
}
