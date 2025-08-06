using System;
using System.Windows.Forms;
using Clicker.ClickerPages.Frontend;

namespace Adventure_Works_Desktop_App
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainClickerForm("admin"));
        }
    }
}
