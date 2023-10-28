using BonchMark;

namespace BonchMarkBlazor.XamlPages;

public partial class Timetable : TabbedPage
{
	internal static DayInfo Monday { get; private set; }
	internal static DayInfo Tuesday { get; private set; }
    internal static DayInfo Wednesday { get; private set; }
    internal static DayInfo Thursday { get; private set; }
    internal static DayInfo Friday { get; private set; }
    internal static DayInfo Saturday { get; private set; }

    public Timetable()
	{
		InitializeComponent();
		foreach (var day in App.week)
		{
			switch (day.Date.DayOfWeek)
			{
				case DayOfWeek.Monday:
					Monday = day; 
					break;
                case DayOfWeek.Tuesday:
                    Tuesday = day;
                    break;
                case DayOfWeek.Wednesday:
                    Wednesday = day;
                    break;
                case DayOfWeek.Thursday:
                    Thursday = day;
                    break;
                case DayOfWeek.Friday:
                    Friday = day;
                    break;
                case DayOfWeek.Saturday:
                    Saturday = day;
                    break;

            }
		}
	}
}