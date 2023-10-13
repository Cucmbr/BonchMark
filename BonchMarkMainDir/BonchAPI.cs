using HtmlAgilityPack;
using System.Net;

namespace BonchMark
{
    internal class BonchAPI
    {
        private HttpClient _httpClient;
        public string Ivan { get { return "users=vanvanich531%40gmail.com&parole=2MFPBNG8RHB"; } }
        private string Tima { get { return "users=tsolomatin1704%40gmail.com&parole=Pidor228"; } }
        private enum MarkState
        {
            NoButton,
            UpdateOnly,
            IncorrectOpenZan,
            requestFailed,
            OK

        }

        public BonchAPI()
        {
            _httpClient = new(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }, true)
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
            using StringContent loginRequest = new StringContent($"users={users}&parole={parole}", null, "application/x-www-form-urlencoded");
            using var loginResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest);
            if (loginResponse.StatusCode != HttpStatusCode.OK)
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
            using StringContent timetableRequest = new StringContent("key=6118", null, "application/x-www-form-urlencoded");
            using var timetableResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest);
            if (timetableResponse.StatusCode != HttpStatusCode.OK)
            {
                return "";
            }
            string text = await timetableResponse.Content.ReadAsStringAsync();
            return text;
        }
        private HtmlDocument CreateHtmlDoc(string content)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(content);
            return htmlDoc;
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
                using StringContent markRequest = new StringContent($"open=1&rasp={openZanArr[0]}&week=={openZanArr[1]}", null, "application/x-www-form-urlencoded");
                using var markResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", markRequest);
                if (markResponse.StatusCode != HttpStatusCode.OK)
                {
                    return MarkState.requestFailed;
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
        public async Task Test()
        {
            //_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");

            //using var getResponse = await _httpClient.GetAsync("https://lk.sut.ru/");
            //string getResponseText = await getResponse.Content.ReadAsStringAsync();
            ////Console.WriteLine(getResponseText);

            //using StringContent loginRequest = new StringContent(_tima, null, "application/x-www-form-urlencoded");
            //using var loginResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest);
            //string loginResponseText = await loginResponse.Content.ReadAsStringAsync();
            //Console.WriteLine("Login:\n" + loginResponseText);
            //Console.WriteLine(loginResponse.StatusCode);
            //Console.WriteLine("--------------------------------------------------------------------");

            //using StringContent timetableRequest = new StringContent("key=6118", null, "application/x-www-form-urlencoded");
            //using var timetableResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest);
            //string responseText = await timetableResponse.Content.ReadAsStringAsync();
            ////Console.WriteLine("Timetable:\n" + responseText);
            //Console.WriteLine(timetableResponse.StatusCode);
            //Console.WriteLine("--------------------------------------------------------------------");

            //var doc = new HtmlDocument();
            //doc.LoadHtml(responseText);
            //var doc = CreateHtmlDoc(await PullTimetable());
            //string? openZan = doc.DocumentNode.SelectSingleNode("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td/span/a").OuterHtml;
            //openZan = openZan.Substring(21, 8);
            //string[] openZanArr = openZan.Split(',');
            //if (openZanArr.Length != 2)
            //{
            //    Console.WriteLine("Benro");
            //    return;
            //}
            //using StringContent markRequest = new StringContent($"open=1&rasp={openZanArr[0]}&week=={openZanArr[1]}", null, "application/x-www-form-urlencoded");
            //using var markResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", markRequest);
            //string markResponseText = await markResponse.Content.ReadAsStringAsync();
            //Console.WriteLine("Mark:\n" + markResponseText);
            //Console.WriteLine(markResponse.StatusCode);
            //Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}
