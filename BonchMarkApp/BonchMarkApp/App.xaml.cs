using Xamarin.Forms;
using System.IO;
using System;
using ;

namespace BonchMarkApp
{
    public partial class App : Application
    {
        static internal string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static internal string[] files;
        static internal BonchMark.

        public App()
        {
            InitializeComponent();
        }

        protected override void OnStart()
        {
            files = Directory.GetFiles(folderPath);
            if(files.Length != 1 || File.ReadAllText(files[0]).Length == 0)
            {
                MainPage = new LoginPage();
            }
            else
            {
                MainPage = new MainPage();
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
