using System;
using System.Reflection;
using System.Windows.Forms;

namespace AFMdraw
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // from http://www.codeproject.com/Articles/528178/Load-DLL-From-Embedded-Resource
            string resource1 = "AFMdraw.AutoUpdater.NET.dll";
            EmbeddedAssembly.Load(resource1, "AutoUpdater.NET.dll");
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}