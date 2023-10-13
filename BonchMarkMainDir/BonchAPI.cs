namespace BonchMark
{
    internal class BonchAPI
    {
        private readonly HttpClient _httpClient;
        public HttpClient HttpClient 
        { 
            get => _httpClient;
        }
        public BonchAPI()
        {
            _httpClient = new(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.None })
            {
                BaseAddress = new Uri("https://lk.sut.ru")
            };

            //client.DefaultRequestHeaders.Add("Host", "lk.sut.ru");
            //client.DefaultRequestHeaders.Add("Accept", "text/plain, */*; q=0.01");
            //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            //client.DefaultRequestHeaders.Add("Accept-Language", "ru,en;q=0.9");
            //client.DefaultRequestHeaders.Add("Origin", "https://lk.sut.ru");
            //client.DefaultRequestHeaders.Referrer = new Uri("https://lk.sut.ru/cabinet/?login=no");
            //client.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"YaBrowser\";v=\"23\"");
            //client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
            //client.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
            //client.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
            //client.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
            //client.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-origin");
            //client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");
            //client.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
        }
        public async Task Test()
        {
            using StringContent content = new StringContent("users=tsolomatin1704%40gmail.com&parole=Pidor228");
            using var response = await HttpClient.PostAsync(_httpClient.BaseAddress + "/cabinet/lib/autentificationok.php", content);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Login:\n" + responseText);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");

            using StringContent content2 = new StringContent("key=6118");
            using var response2 = await HttpClient.PostAsync(this.HttpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", content2);
            Console.WriteLine(response2.StatusCode);
            string responseText2 = await response2.Content.ReadAsStringAsync();
            Console.WriteLine(responseText2);
            Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}
