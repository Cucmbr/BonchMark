using AngleSharp.Html.Dom;
using System.Linq;
using BonchMark;
using AngleSharp.Dom;

namespace BonchMark
{
    public class Timetable
    {
        private IHtmlDocument _fullTimetable;
        private IElement _tableNode;
        private BonchAPI _api;

        private Timetable(IHtmlDocument html, BonchAPI api)
        {
            _fullTimetable = html;
            _tableNode = _fullTimetable.QuerySelector("tbody");
            _api = api;
        }
        public static async Task<Timetable> CreateAsync(BonchAPI api)
        {
            return new Timetable(api.Parser.ParseDocument(await api.PullTimetableAsync()), api);
        }

        public async Task Update()
        {
            _fullTimetable = _api.Parser.ParseDocument(await _api.PullTimetableAsync());
        }

        public List<ClassInfo> GetClasses()
        {
            List<ClassInfo> classes = new List<ClassInfo>();
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
                    for  (int j = 0; j < dayInf[i]; j++) 
                    {
                        classes.Add(new ClassInfo(dateNodes[i].TextContent, timeNodes[infCtr].TextContent, nameNodes[infCtr].TextContent, typeNodes[infCtr].TextContent, placeNodes[infCtr].TextContent, teacherNodes[infCtr].TextContent));
                        infCtr++;
                    }
                }

            }

            return classes;
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
        public string Date { get; }
        public string Time { get; }
        public string Name { get; }
        public string Type { get; }
        public string Place { get; }
        public string Teacher { get; }

        public ClassInfo(string date, string time, string name, string type, string place, string teacher)
        {
            Date =  date;
            Time = time;
            Name = name;
            Type = type;
            Place = place;
            Teacher = teacher;
        }
    }

}
