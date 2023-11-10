using BonchMark;

namespace BonchMarkBlazor
{
    public partial class App : Application
    {
        internal static readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        internal static string[] Files;
        internal static BonchAPI Api = new BonchAPI();
        internal static Timetable Timetable;
		internal static List<DayInfo> Week;
        internal static Task<bool> Starting;
        internal static Task<BonchAPI.MarkStatus> Marking;
        internal static Task<bool> Timetabling;

		internal static string[] userData;

        internal static DayInfo Monday { get; set; }
        internal static DayInfo Tuesday { get; set; }
        internal static DayInfo Wednesday { get; set; }
        internal static DayInfo Thursday { get; set; }
        internal static DayInfo Friday { get; set; }
        internal static DayInfo Saturday { get; set; }

        public App()
        {
            InitializeComponent();

            Files = Directory.GetFiles(FolderPath, "*.txt");
            
            MainPage = new NavigationPage(new MainPage());

            if (File.Exists(Path.Combine(FolderPath, "UserData.txt")))
                userData = File.ReadAllText(Path.Combine(FolderPath, "UserData.txt")).Split(":");

            Starting = StartAsync();
            Marking = MarkAsync();
            Timetabling = TimetableAsync();
        }

        static private async Task<bool> StartAsync()
        {
            if(await Api.InitAsync() && await Api.LoginAsync(userData[0], userData[1]))
                return true;
            else
                return false;
        }

        static private async Task<BonchAPI.MarkStatus> MarkAsync()
        {
            if (await Starting)
                return await Api.MarkSequenceAsync();
            else
                return BonchAPI.MarkStatus.RequestFailed;
		}

        static private async Task<bool> TimetableAsync()
        {
            if (await Starting)
            {
                Timetable = await Timetable.CreateAsync(Api);
                Week = Timetable.GetWeek();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}