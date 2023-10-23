using HtmlAgilityPack;
using System.Net;

namespace BonchMark
{
    public class BonchAPI
    {
        private HttpClient _httpClient;
        private string _users;
        private string _parole;
        
        private class LkRequest : StringContent
        {
            public LkRequest(string content)
                : base(content, null, "application/x-www-form-urlencoded") { }
        }

        public enum MarkStatus
        {
            NoButton,
            UpdateOnly,
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

        public async Task<bool> InitAsync()
        {
            using (var initResponse = await _httpClient.GetAsync("https://lk.sut.ru/"))
            {
                if (initResponse != null) 
                {
                    return initResponse.IsSuccessStatusCode;
                }
                return false;
            }
        }

        public async Task<bool> LoginAsync(string users, string parole)
        {
            using LkRequest loginRequest = new LkRequest($"users={users}&parole={parole}");
            using (var loginResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest))
            {
                if (InvalidResponseCheck(loginResponse))
                {
                    return false;
                }
                string loginResponseText = await loginResponse.Content.ReadAsStringAsync();
                if (loginResponseText == "1")
                {
                    _users = users;
                    _parole = parole;
                    return true;
                }
                return false;
            }
        }

        public async Task<string> PullTimetableAsync()
        {
            using LkRequest timetableRequest = new LkRequest("key=6118");
            using (var timetableResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest))
            {
                if (InvalidResponseCheck(timetableResponse))
                {
                    return "";
                }
                string text = await timetableResponse.Content.ReadAsStringAsync();
                return text;
            }
        }

        private async Task<MarkStatus> MarkAsync()
        {
            var doc = CreateHtmlDoc(await PullTimetableAsync());
            var openZanNode = doc.DocumentNode.SelectSingleNode("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td/span/a");
            if (openZanNode != null)
            {
                string openZan = openZanNode.OuterHtml;
                openZan = openZan.Substring(openZan.IndexOf("(") + 1, openZan.IndexOf(")") - openZan.IndexOf("(") - 1);
                string[] openZanArr = openZan.Split(',');
                if (openZanArr.Length != 2)
                {
                    return MarkStatus.UpdateOnly;
                }
                using LkRequest markRequest = new LkRequest($"open=1&rasp={openZanArr[0]}&week=={openZanArr[1]}");
                using (var markResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", markRequest))
                {
                    string markResponseText = await markResponse.Content.ReadAsStringAsync();
                    if (InvalidResponseCheck(markResponse) || markResponseText.Contains("id:0"))
                    {
                        return MarkStatus.RequestFailed;
                    }
                    return MarkStatus.OK;
                }
            }
            else
                return MarkStatus.NoButton;
        }

        internal HtmlDocument CreateHtmlDoc(string content)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            return htmlDoc;
        }

        private bool InvalidResponseCheck(HttpResponseMessage message)
        {
            if (message == null || message.StatusCode != HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public async Task<MarkStatus> MarkSequenceAsync()
        {
            if (await InitAsync() && await LoginAsync(_users, _parole))
                return await MarkAsync();
            else
                return MarkStatus.RequestFailed;
        }
    }
}
