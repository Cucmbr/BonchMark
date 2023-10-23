//using BonchMark;
//using HtmlAgilityPack;

//BonchAPI api = new BonchAPI();
//await api.InitAsync();
//await api.LoginAsync("vanvanich531@gmail.com", "2MFPBNG8RHB");
//var time = await Timetable.CreateAsync(api);

//Console.WriteLine(time.GetClasses());

//namespace BonchMark
//{
//    public class Timetable
//    {
//        private HtmlDocument _fullTimetable;

//        private Timetable(HtmlDocument html)
//        {
//            _fullTimetable = html;
//        }
//        public static async Task<Timetable> CreateAsync(BonchAPI api)
//        {
//            return new Timetable(api.CreateHtmlDoc(await api.PullTimetableAsync()));
//        }

//        public List<ClassInfo> GetClasses()
//        {
//            List<ClassInfo> classes = new List<ClassInfo>();
//            HtmlNodeCollection dateNodes = _fullTimetable.DocumentNode.SelectNodes("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td[@colspan='6']/small");
//            HtmlNodeCollection nameNodes = _fullTimetable.DocumentNode.SelectNodes("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td[not(self::td[@colspan='6'])]/b");


//            string tbody = _fullTimetable.DocumentNode.SelectSingleNode("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody").OuterHtml;
//            string tempbody = tbody.Substring(tbody.IndexOf("tr style=\"background: #b3b3b3"), tbody.Length - tbody.TrimStart('<').IndexOf("<tr style=\"background: #b3b3b3"));
//            tbody.TrimStart()
//            //for (int i = 0; i < nameNodes.Count; i++)
//            //{
//            //    classes.Add(new ClassInfo());
//            //}
//            return classes;
//        }
//    }

//    public class ClassInfo
//    {
//        public string Date { get; }
//        public string Number { get; }
//        public string Name { get; }
//        public string Type { get; }
//        public string Place { get; }
//        public string Teacher { get; }

//        public ClassInfo(string date, string number, string name, string type, string place, string teacher)
//        {
//            Date = date;
//            Number = number;
//            Name = name;
//            Type = type;
//            Place = place;
//            Teacher = teacher;
//        }

//        public override string ToString()
//        {
//            return $"Date: {Date} Number: {Number} Name: {Name} Type: {Type} Place: {Place} Teacher: {Teacher}";
//        }
//    }

//}
