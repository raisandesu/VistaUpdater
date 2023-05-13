using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace VistaUpdater
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Microsoft.Win32.RegistryKey regkey1 =
                 Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\VistaUpdater", false);
            if (regkey1 == null)
            {
                Application.Run(new Form1());
                return;
            }
            long s = (long)regkey1.GetValue("Restarted");
            regkey1.Close();
            if (s == 1)
            {
              Application.Run(new RestartedUpdate());
            }
            else if (s == 2)
            {
                Application.Run(new RestartedUpdate2());
            } else if (s == 3)
            {
                Application.Run(new RestartedUpdate3());
            } else if (s == 4)
            {
                Application.Run(new Final());
            }
            else
            {
              Application.Run(new Form1());
            }
        }
    }
}
