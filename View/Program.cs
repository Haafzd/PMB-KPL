using System;
using System.Windows.Forms;

namespace View
{
    internal static class Program
    {
        /// <summary>
        /// Titik masuk utama aplikasi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new LoginForm()); // ganti Form1 dengan form awal aplikasi
        }
    }
}
