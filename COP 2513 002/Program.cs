/*
 * Quinn Berichon
 * Program Class
 * 4/18/2023
 */

using System;
using System.Windows.Forms;

namespace COP_2513_002
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
            Application.Run(new FormStockLoader());
        }
    }
}