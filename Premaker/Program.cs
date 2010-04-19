using System;
using System.Windows.Forms;
using com.jds.Premaker.classes.forms;

namespace com.jds.Premaker
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChooseForm());
        }
    }
}
