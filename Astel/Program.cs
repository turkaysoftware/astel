using System;
using System.Windows.Forms;
//
using static Astel.TSModules;
using Astel.astel_modules;

namespace Astel{
    internal static class Program{
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main(){
            if (Environment.OSVersion.Version.Major >= 6){ SetProcessDPIAware(); }
            //
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // DYNAMIC STARTUP
            TSSettingsSave software_read_settings = new TSSettingsSave(ts_sf);
            string session_mode = software_read_settings.TSReadSettings(ts_settings_container, "SessionMode");
            switch (session_mode){
                case "0":
                    Application.Run(new Astel());
                    break;
                case "1":
                    Application.Run(new AstelLogin());
                    break;
                default:
                    Application.Run(new Astel());
                    break;
            }
        }
    }
}