using BonchMark;

namespace BonchMarkBlazor
{
    public partial class App : Application
    {
        static internal string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static internal string[] files;
        static internal BonchAPI api = new BonchAPI();
        static internal Timetable timetable;
        static internal List<DayInfo> week;

        public App()
        {
            InitializeComponent();

            files = Directory.GetFiles(folderPath, "*.txt");
            
            MainPage = new AppShell();
        }
    }
}