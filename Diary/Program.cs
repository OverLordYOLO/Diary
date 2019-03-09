using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Diary
{
    static class Global
    {
        private static ClassFactory _classFactory = null;
        public static ClassFactory classFactory
        {
            get
            {
                return Global._classFactory;
            }
            set
            {
                if (Global._classFactory == null)
                {
                    Global._classFactory = value;
                }
                else
                {
                    throw new InvalidOperationException("Property classFactory can be set only once.");
                }
            }
        }
    }
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XDocument settingsFromFile = XDocument.Load(@".\classFactorySettings.xml");
            Global.classFactory = new ClassFactory(settingsFromFile);


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
