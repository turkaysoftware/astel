using System;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
//
using static Astel.TSModules;

namespace Astel{
    internal static class Program{
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        // ======================================================================================================
        // GLOBAL TS CANCEL TOKEN
        public static CancellationTokenSource TS_TokenEngine;
        // ======================================================================================================
        // GLOBAL SYSTEM INFO
        public static int windows_mode = 0;
        // ======================================================================================================
        // TS PRELOADER DEBUG MODE
        public static readonly bool ts_pre_debug_mode = false;
        // ======================================================================================================
        [STAThread]
        static void Main(){
            SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            // ------------------------------------------------------------------
            // CHECK WINDOWS VERSION
            try{
                using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT Caption FROM Win32_OperatingSystem"))
                using (var results = searcher.Get()){
                    string caption = results.Cast<ManagementObject>().Select(mo => mo["Caption"]?.ToString()).FirstOrDefault();
                    windows_mode = (caption?.IndexOf("Windows 11", StringComparison.OrdinalIgnoreCase) >= 0) ? 1 : 0;
                }
            }catch (Exception){ }
            // ------------------------------------------------------------------
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TSPreloader());
        }
    }
}