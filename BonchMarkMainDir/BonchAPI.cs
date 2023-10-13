using HtmlAgilityPack;
using System.Reflection.Metadata;

namespace BonchMark
{
    internal class BonchAPI
    {
        private HttpClient _httpClient;
        private const string _ivan = "users=vanvanich531%40gmail.com&parole=2MFPBNG8RHB";
        private const string _tima = "users=tsolomatin1704%40gmail.com&parole=Pidor228";

        public BonchAPI()
        {
            _httpClient = new(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }, true)
            {
                BaseAddress = new Uri("https://lk.sut.ru")
            };
        }

        public async Task Test()
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");

            using var getResponse = await _httpClient.GetAsync("https://lk.sut.ru/");
            string getResponseText = await getResponse.Content.ReadAsStringAsync();
            //Console.WriteLine(getResponseText);

            using StringContent loginRequest = new StringContent(_tima, null, "application/x-www-form-urlencoded");
            using var loginResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest);
            string loginResponseText = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login:\n" + loginResponseText);
            Console.WriteLine(loginResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");

            using StringContent timetableRequest = new StringContent("key=6118", null, "application/x-www-form-urlencoded");
            using var timetableResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest);
            string responseText = await timetableResponse.Content.ReadAsStringAsync();
            //Console.WriteLine("Timetable:\n" + responseText);
            Console.WriteLine(timetableResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");

            var doc = new HtmlDocument();
            doc.LoadHtml(responseText);
            string openZan = doc.DocumentNode.SelectSingleNode("/div[@class='container-fluid']/table[@class='simple-little-table']/tbody/tr/td/span/a").OuterHtml;
            openZan = openZan.Substring(21, 8);
            string[] openZanArr = openZan.Split(',');
            if (openZanArr.Length != 2)
            {
                Console.WriteLine("Benro");
                return;
            }
            using StringContent markRequest = new StringContent($"open=1&rasp={openZanArr[0]}&week=={openZanArr[1]}", null, "application/x-www-form-urlencoded");
            using var markResponse = await _httpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", markRequest);
            string markResponseText = await markResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Mark:\n" + markResponseText);
            Console.WriteLine(markResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}
