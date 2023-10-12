using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace BonchMark
{
    class Program
    {
        private static readonly HttpClient client = new(new HttpClientHandler { AllowAutoRedirect = false })
        {

            BaseAddress = new Uri("https://lk.sut.ru")
        };

        static async Task Main()
        {
            //Args args = new Args { users = "@gmail.com", parole = ""};
            //JsonContent content = JsonContent.Create(args);

            client.DefaultRequestHeaders.Add("Host", "lk.sut.ru");
            client.DefaultRequestHeaders.Add("Accept", "text/plain, */*; q=0.01");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            client.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
            client.DefaultRequestHeaders.Add("Origin", "https://lk.sut.ru");
            client.DefaultRequestHeaders.Referrer = new Uri("https://lk.sut.ru/cabinet/?login=no");
            client.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"YaBrowser\";v=\"23\"");
            client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
            client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");
            client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");


            using StringContent content = new StringContent("users=tsolomatin1704%40gmail.com&parole=Pidor228");
            using var response = await client.PostAsync(client.BaseAddress + "/cabinet/lib/autentificationok.php", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(response);

            using StringContent content2 = new StringContent("key=6118");
            using var response2 = await client.PostAsync(client.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", content2);

            var doc = new HtmlAgilityPack.HtmlDocument();

            string response2Text = await response2.Content.ReadAsStringAsync();
            var html = await response2.Content.ReadAsStringAsync();
            doc.Load(html);
            var h = doc.DocumentNode.SelectSingleNode(".//h3");
            if (h != null)
            {
                Console.WriteLine(h.InnerHtml);
            }

            //Console.WriteLine((int)response2.StatusCode);
            //Console.WriteLine(response2Text);
        }

        class Args
        {
            public string users { get; set; }
            public string parole { get; set; }
        }
    }
}