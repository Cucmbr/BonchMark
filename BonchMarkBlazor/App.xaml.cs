using BonchMark;

namespace BonchMarkBlazor;

public partial class App : Application
{
    internal static readonly string FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    internal static string[] Files;
    internal static BonchAPI Api = new BonchAPI();
    internal static Timetable Timetable;
    internal static Messages Messages;
	internal static List<DayInfo> Days;
    internal static Task<bool> Starting;
    internal static Task<BonchAPI.MarkStatus> Marking;
    internal static Task<bool> Timetabling;
    internal static Task<bool> MessLoading;

	internal static string[] userData;

    internal static DayInfo[] Week { get; set; } = new DayInfo[6];

	public App()
    {
        InitializeComponent();

        Files = Directory.GetFiles(FolderPath, "*.txt");
        
        MainPage = new NavigationPage(new MainPage());

        if (File.Exists(Path.Combine(FolderPath, "UserData.txt")))
        {
            userData = File.ReadAllText(Path.Combine(FolderPath, "UserData.txt")).Split(":");
            Starting = StartAsync();
            Timetabling = TimetableAsync();
        }
    }

    static internal async Task<bool> StartAsync()
    {
        if(await Api.InitAsync() && await Api.LoginAsync(userData[0], userData[1]))
            return true;
        else
            return false;
    }

    static internal async Task<BonchAPI.MarkStatus> MarkAsync()
    {
        if (await Starting)
            return await Api.MarkAsync();
        else
            return BonchAPI.MarkStatus.RequestFailed;
	}

    static internal async Task<bool> TimetableAsync()
    {
        if (await Starting)
        {
            Timetable = await Timetable.CreateAsync(Api);
            return true;
        }
        else
            return false;
    }

    static internal async Task<bool> MessLoadAsync()
    {
        if (await Starting)
        {
            Messages = await Messages.CreateAsync(Api);
            return true;
        }
        else
            return false;
    }

    static internal async Task<DayInfo[]> GenerateWeekAsync(int weekNumber)
    {
        DayInfo[] resultWeek = new DayInfo[6];
        if (await Timetabling)
        {
            await Timetable.Update(weekNumber);
            Days = Timetable.GetWeek();

            if (Days.Count == 0)
            {
                for (int i = 0; i < resultWeek.Length; i++)
                {
                    resultWeek[i] = new DayInfo() { Date = new DateTime(1, 1, 1) };
                }
                return resultWeek; // vonaet nado dodelat
            }

            foreach (var day in Days)
            {
                switch (day.Date.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        resultWeek[0] = day;
                        break;
                    case DayOfWeek.Tuesday:
                        resultWeek[1] = day;
                        break;
                    case DayOfWeek.Wednesday:
                        resultWeek[2] = day;
                        break;
                    case DayOfWeek.Thursday:
                        resultWeek[3] = day;
                        break;
                    case DayOfWeek.Friday:
                        resultWeek[4] = day;
                        break;
                    case DayOfWeek.Saturday:
                        resultWeek[5] = day;
                        break;
                }
            }

            for (int i = 0; i < resultWeek.Length; i++)
            {
                if (resultWeek[i] == null)
                {
                    if (i == 0)
                    {
                        int j = 1;
                        while (resultWeek[j] == null)
                        {
                            j++;
                        }
                        resultWeek[i] = new DayInfo() { Date = resultWeek[j].Date.AddDays(-j) };
                    }
                    else
                    {
                        int j = i;
                        while (resultWeek[j] == null)
                        {
                            j--;
                        }
                        resultWeek[i] = new DayInfo() { Date = resultWeek[j].Date.AddDays(i - j) };
                    }
                }
            }
        }
        return resultWeek;
    }
}