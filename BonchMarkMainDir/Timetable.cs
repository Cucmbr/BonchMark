using HtmlAgilityPack;

namespace BonchMark
{
    public class Timetable
    {
        private HtmlDocument _fullTimetable;
        private Timetable(HtmlDocument html)
        {
            _fullTimetable = html;
        }

        public static async Task<Timetable> CreateAsync(BonchAPI api)
        {
            return new Timetable(api.CreateHtmlDoc(await api.PullTimetableAsync()));
        }
    }
}
