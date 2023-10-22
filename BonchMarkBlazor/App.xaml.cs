using BonchMark;

namespace BonchMarkBlazor
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
            
            MainPage = new MainPage();
        }
    }
}