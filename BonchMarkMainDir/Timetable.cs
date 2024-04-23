using AngleSharp.Html.Dom;
using AngleSharp.Dom;

namespace BonchMark;

public class Timetable
{
    public int CurrentWeek { get; } = 0;

    private IHtmlDocument _fullTimetable;
    private IElement _tableNode;
    private readonly BonchAPI _api;

    private Timetable(IHtmlDocument html, BonchAPI api)
    {
        _fullTimetable = html;
        _tableNode = _fullTimetable.QuerySelector("tbody");
        _api = api;

        var weekText = _fullTimetable.QuerySelector("div.container-fluid > h3").TextContent;
        CurrentWeek = Convert.ToInt32(weekText.Substring(weekText.IndexOf('№') + 1, weekText.IndexOf('(') - weekText.IndexOf('№') - 1));
    }
    public static async Task<Timetable> CreateAsync(BonchAPI api) => new Timetable(api.Parser.ParseDocument(await api.PullTimetableAsync(0)), api);

    public async Task Update(int weekNumber)
    {
        _fullTimetable = _api.Parser.ParseDocument(await _api.PullTimetableAsync(weekNumber));
        _tableNode = _fullTimetable.QuerySelector("tbody");
    }

    public List<DayInfo> GetWeek()
    {
        List<DayInfo> week = new List<DayInfo>();
        if(_tableNode != null)
        {
            var dateNodes = _tableNode.QuerySelectorAll("tr td[colspan] small");
            var timeNodes = _tableNode.QuerySelectorAll("tr td small[style]");
            var nameNodes = _tableNode.QuerySelectorAll("tr td[align] b");
            var typeNodes = _tableNode.QuerySelectorAll("tr td[align] small");
            var placeNodes = _tableNode.QuerySelectorAll("tr td:nth-child(4)");
            var teacherNodes = _tableNode.QuerySelectorAll("tr td:nth-child(5)");

            var dayInf = DayClassCount();
            int infCtr = 0;

            for(int i = 0; i < dateNodes.Length; i++)
            {
                var classes = new List<ClassInfo>();
                for  (int j = 0; j < dayInf[i]; j++) 
                {
                    classes.Add(new ClassInfo(timeNodes[infCtr].TextContent.Split(" "), nameNodes[infCtr].TextContent, typeNodes[infCtr].TextContent.Split("  "), placeNodes[infCtr].TextContent, teacherNodes[infCtr].TextContent));
                    infCtr++;
                }
                week.Add(new DayInfo { Classes = classes, Date = ParseDate(dateNodes[i].TextContent) });
            }

        }

        return week;
    }

    internal DateTime ParseDate(string Date)
    {
        return DateTime.ParseExact(Date, "dd.MM.yyyy", null);
    }

    private List<int> DayClassCount()
    {
        List<int> classNumDay = new();
        var trs = _tableNode.QuerySelectorAll("tr");
        int counter = 0;
        foreach (var node in trs)
        {
            if (node.GetAttribute("style") == "background: #b3b3b3; !important; ")
            {
                if (counter == 0) continue; 
                classNumDay.Add(counter);
                counter = 0;
                continue;
            }
            counter++;
        }
        if (counter != 0) classNumDay.Add(counter);
        return classNumDay;
    }
}

public class ClassInfo
{
    public string[] Time { get; }
    public string Name { get; }
    public string[] Type { get; }
    public string Place { get; }
    public string Teacher { get; }

    public ClassInfo(string[] time, string name, string[] type, string place, string teacher)
    {
        Time = time.Length == 2 ? new string[] { time[0], time[1].Substring(time[1].IndexOf("(") + 1, time[1].IndexOf(")") - time[1].IndexOf("(") - 1) } : new string[] {"ФЗ", time[0] };
        Name = name;
        Type = type.Length == 2 ? type : new string[] { type[0], null };
        Place = place;
        Teacher = teacher;
    }
}

public class DayInfo
{
    public DateTime Date { get; set; }
    public List<ClassInfo> Classes { get; internal set; }
}
