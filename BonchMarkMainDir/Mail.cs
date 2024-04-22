using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace BonchMark;

public class Mail
{

    public int CurrentPage { get; set; } = 1;
    public List<MailItem> Items { get; set; }

    private IHtmlDocument _fullMailPage;
    private IElement _mailTableNode;
    private readonly BonchAPI _api;

    private Mail(IHtmlDocument html, BonchAPI api) // полностью не готово
    {
        _fullMailPage = html;
        _mailTableNode = _fullMailPage.QuerySelector("tbody");
        _api = api;

        var pageText = _fullMailPage.QuerySelector("div.container-fluid > h3").TextContent;
        CurrentPage = Convert.ToInt32(pageText.Substring(pageText.IndexOf('№') + 1, pageText.IndexOf('(') - pageText.IndexOf('№') - 1));
    }
    public static async Task<Mail> CreateAsync(BonchAPI api, bool isMessages)
    {
        switch(isMessages)
        {
            case true:
                return new Mail(api.Parser.ParseDocument(await api.PullMessagesAsync()), api);
            case false:
                return new Mail(api.Parser.ParseDocument(await api.PullMessagesAsync()), api); // нужно будет заменить на метод для получения html файлов группы

        }
    }

    public void LoadItems()
    {
        Items = new List<MailItem>(20);

    }

    public class MailItem
    {
        public DateTime Date { get; }
        public string Subject { get; }
        public string Files { get; }
        public string Destination { get; }

        public MailItem(DateTime date, string subject, string files, string destination)
        {
            Date = date;
            Subject = subject;
            Files = files;
            Destination = destination;
        }
    }
}
