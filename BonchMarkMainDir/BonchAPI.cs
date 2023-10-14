using HtmlAgilityPack;
using System.Net;

namespace BonchMark
{
    public class BonchAPI
    {
        private HttpClient _httpClient;

        private string[] _ivan = { "vanvanich531%40gmail.com", "2MFPBNG8RHB" }; //For testing ONLY. It's not a final solution!
        private string[] _tima = { "tsolomatin1704%40gmail.com", "Pidor228" };
        private class LkRequest : StringContent
        {
            public LkRequest(string content)
                : base(content, null, "application/x-www-form-urlencoded") { }
        }

        private enum MarkState
        {
            NoButton,
            UpdateOnly,
            IncorrectOpenZan,
            RequestFailed,
            OK
        }

        public BonchAPI()
        {
            _httpClient = new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }, true)
            {
                BaseAddress = new Uri("https://lk.sut.ru")
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");
        }

        private async Task<bool> Init()
        {
            using var initResponse = await _httpClient.GetAsync("https://lk.sut.ru/");
            if (initResponse != null) 
            {
                return initResponse.IsSuccessStatusCode;
            }
            return false;
        }
        private async Task<bool> Login(string users, string parole)
        {
            using LkRequest loginRequest = new LkRequest($"users={users}&parole={parole}");
            using var loginResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest);
            if (InvalidResponseCheck(loginResponse))
            {
                return false;
            }
            string loginResponseText = await loginResponse.Content.ReadAsStringAsync();
            if (loginResponseText == "1")
                return true;
            return false;
        }
        private async Task<string> PullTimetable()
        {
            using LkRequest timetableRequest = new LkRequest("key=6118");
            using var timetableResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest);
            if (InvalidResponseCheck(timetableResponse))
            {
                return "";
            }
            string text = await timetableResponse.Content.ReadAsStringAsync();
            return text;
        }
        private async Task<MarkState> Mark()
        {
            var doc = CreateHtmlDoc(await PullTimetable());
            var openZanNode = doc.DocumentNode.SelectSingleNode("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td/span/a");
            if (openZanNode != null)
            {
                string openZan = openZanNode.OuterHtml;
                openZan = openZan.Substring(21, openZan.IndexOf(")") - openZan.IndexOf("("));
                string[] openZanArr = openZan.Split(',');
                if (openZanArr.Length != 2)
                {
                    return MarkState.IncorrectOpenZan;
                }
                using LkRequest markRequest = new LkRequest($"open=1&rasp={openZanArr[0]}&week=={openZanArr[1]}");
                using var markResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", markRequest);
                if (InvalidResponseCheck(markResponse))
                {
                    return MarkState.RequestFailed;
                }
                string markResponseText = await markResponse.Content.ReadAsStringAsync();
                if (markResponseText.Contains("id:0"))
                {
                    return MarkState.UpdateOnly;
                }
                else
                    return MarkState.OK;
            }
            else
                return MarkState.NoButton;
        }
        private HtmlDocument CreateHtmlDoc(string content)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            return htmlDoc;
        }
        private bool InvalidResponseCheck(HttpResponseMessage message)
        {
            if (message == null && message.StatusCode != HttpStatusCode.OK)
                return true;
            else
                return false;
        }
        public async Task MarkSequence() //For testing ONLY. It's not a final solution!
        {
            await Init();
            await Login(_tima[0], _tima[1]);
            await Mark();

        }
    }
}
