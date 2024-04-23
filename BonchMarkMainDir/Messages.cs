using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace BonchMark;

public class Messages
{

    public int CurrentPage { get; set; } = 1;
    public List<MailItem> Items { get; set; }

    private IHtmlDocument _fullMailPage;
    private IElement _mailTableNode;
    private readonly BonchAPI _api;

    private Messages(IHtmlDocument html, BonchAPI api)
    {
        _fullMailPage = html;
        _mailTableNode = _fullMailPage.QuerySelector("tbody");
        _api = api;

        CurrentPage = Convert.ToInt32(_fullMailPage.QuerySelector("#table_mes > center > b").TextContent);

        LoadItems();
    }
    public static async Task<Messages> CreateAsync(BonchAPI api) => new Messages(api.Parser.ParseDocument(await api.PullMessagesAsync()), api);

    public void LoadItems()
    {
        Items = new List<MailItem>(20);

        if (_mailTableNode != null)
        {
            var itemNodes = _mailTableNode.QuerySelectorAll("tr");

            foreach (var itemNode in itemNodes)
            {
                var itemData = itemNode.QuerySelectorAll("td");

                Items.Add(new MailItem(itemData[0].TextContent, itemData[1].TextContent, itemData[2].TextContent, itemData[3].TextContent));
            }
        }
    }
}
