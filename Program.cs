using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.IO;
namespace CheckFile
{

    internal static class Program
    {

        public static Dictionary<string, string> Lang = new JavaScriptSerializer().Deserialize<Dictionary<string, string>>(File.ReadAllText("Langague.json"));
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
