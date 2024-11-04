using System;
using System.Windows.Forms;
//
using static Astel.TSModules;

namespace Astel{
    internal static class Program{
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        // ======================================================================================================
        // VERSION MODE
        public static int ts_version_mode = 0;
        // ======================================================================================================
        [STAThread]
        static void Main(){
            if (Environment.OSVersion.Version.Major >= 6){ SetProcessDPIAware(); }
            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TSPreloader());
        }
    }
}