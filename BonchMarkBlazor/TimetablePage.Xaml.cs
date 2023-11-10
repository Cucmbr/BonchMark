namespace BonchMarkBlazor;

public partial class TimetablePage : TabbedPage
{
	public TimetablePage()
	{
		InitializeComponent();

		label.Text = $"{App.Week[0].Date.Day}.{App.Week[0].Date.Month} - {App.Week[App.Week.Count - 1].Date.Day}.{App.Week[App.Week.Count - 1].Date.Month}";

		switch (DateTime.Today.DayOfWeek)
		{
			case DayOfWeek.Monday:
                CurrentPage = Children[0];
                break;
            case DayOfWeek.Tuesday:
                CurrentPage = Children[1];
                break;
            case DayOfWeek.Wednesday:
                CurrentPage = Children[2];
                break;
            case DayOfWeek.Thursday:
                CurrentPage = Children[3];
                break;
            case DayOfWeek.Friday:
                CurrentPage = Children[4];
                break;
            case DayOfWeek.Saturday:
                CurrentPage = Children[5];
                break;
        }
	}
}