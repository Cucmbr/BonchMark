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

        internal static DayInfo Monday { get; private set; }
        internal static DayInfo Tuesday { get; private set; }
        internal static DayInfo Wednesday { get; private set; }
        internal static DayInfo Thursday { get; private set; }
        internal static DayInfo Friday { get; private set; }
        internal static DayInfo Saturday { get; private set; }

        public App()
        {
            InitializeComponent();

            files = Directory.GetFiles(folderPath, "*.txt");
            
            MainPage = new AppShell();
        }
    }
}