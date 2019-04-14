using System;
using System.Windows.Forms;

namespace NOS_Kriptografija
{
    static class Program
    {
        public static string Direktorij = Environment.CurrentDirectory + @"\Files\";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HomeWindow());
        }
    }
}
