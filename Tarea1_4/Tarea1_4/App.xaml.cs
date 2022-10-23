using System;
using System.IO;
using Tarea1_4.Controller;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tarea1_4
{
    public partial class App : Application
    {
        static DB db;

        public static DB dba
        {
            get
            {
                if (db == null)
                {
                    String folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "dbaFotos.db3");

                    db = new DB(folderPath);
                }
                return db;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
