using BonchMark;
namespace BonchMarkMAUI
{
    public partial class App : Application
    {
        static internal string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static internal string[] files;
        static internal BonchAPI api = new BonchAPI();

        public App()
        {
            InitializeComponent();

            files = Directory.GetFiles(folderPath, "*.txt");
            if (files.Length != 1 || File.ReadAllText(files[0]).Length == 0)
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new MainPage();
            }
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