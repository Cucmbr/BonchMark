using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace BonchMark;

public class StudFiles // ещё не начинал, так что в основном это просто копия Messages
{

    public int CurrentPage { get; set; } = 1;
    public List<MailItem> Items { get; set; }

    private IHtmlDocument _fullMailPage;
    private IElement _mailTableNode;
    private readonly BonchAPI _api;

    private StudFiles(IHtmlDocument html, BonchAPI api)
    {
        _fullMailPage = html;
        _mailTableNode = _fullMailPage.QuerySelector("tbody");
        _api = api;

        CurrentPage = Convert.ToInt32(_fullMailPage.QuerySelector("#table_mes > center > b").TextContent);
    }
    public static async Task<StudFiles> CreateAsync(BonchAPI api) => new StudFiles(api.Parser.ParseDocument(await api.PullMessagesAsync()), api);

    public void LoadItems()
    {
        Items = new List<MailItem>(20);

    }
}
