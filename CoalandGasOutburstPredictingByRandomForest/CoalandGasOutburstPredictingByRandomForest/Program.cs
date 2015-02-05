using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CoalandGasOutburstPredictingByRandomForest
{
    using RDotNet;
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string RDllDir = Properties.Settings.Default.RDLLDir;
            //REngine.SetDllDirectory(@"C:\Program Files\R\R-2.15.0\bin\i386");
            REngine.SetDllDirectory(RDllDir);
            REngine.CreateInstance("RDotNet");
            
            Application.Run(new frmMain());
        }
    }
}
