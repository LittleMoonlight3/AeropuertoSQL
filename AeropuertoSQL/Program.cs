using System;
using System.Windows.Forms;

namespace AeropuertoSQL
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (GokuLoader loader = new GokuLoader())
                loader.ShowDialog();

            Application.Run(new Form1());
        }
    }
}
