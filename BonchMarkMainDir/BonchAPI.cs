namespace BonchMark
{
    internal class BonchAPI
    {
        public HttpClient HttpClient { get; }

        public BonchAPI()
        {
            HttpClient = new(new HttpClientHandler() { AutomaticDecompression = System.Net.DecompressionMethods.GZip }, true)
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
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.5845.967 YaBrowser/23.9.1.967 Yowser/2.5 Safari/537.36");

            using var getResponse = await HttpClient.GetAsync("https://lk.sut.ru/");
            string getResponseText = await getResponse.Content.ReadAsStringAsync();
            //Console.WriteLine(getResponseText);

            using StringContent loginRequest = new StringContent("users=tsolomatin1704%40gmail.com&parole=Pidor228", null, "application/x-www-form-urlencoded");
            using var loginResponse = await HttpClient.PostAsync(HttpClient.BaseAddress + "/cabinet/lib/autentificationok.php", loginRequest);
            string loginResponseText = await loginResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Login:\n" + loginResponseText);
            Console.WriteLine(loginResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");

            using var getResponse2 = await HttpClient.GetAsync("https://lk.sut.ru/cabinet/?login=yes");
            string getResponseText2 = await getResponse2.Content.ReadAsStringAsync();
            //Console.WriteLine(getResponseText2);

            using StringContent timetableRequest = new StringContent("key=6118", null, "application/x-www-form-urlencoded");
            using var timetableResponse = await HttpClient.PostAsync(HttpClient.BaseAddress + "/cabinet/project/cabinet/forms/raspisanie.php", timetableRequest);
            string responseText = await timetableResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Timetable:\n" + responseText);
            Console.WriteLine(timetableResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");

            using StringContent markRequest = new StringContent("Вставить запрос на отметку!");
            using var markResponse = await HttpClient.PostAsync(HttpClient.BaseAddress + "", markRequest);
            string markResponseText = await markResponse.Content.ReadAsStringAsync();
            //Console.WriteLine("Mark:\n" + markResponseText);
            Console.WriteLine(markResponse.StatusCode);
            Console.WriteLine("--------------------------------------------------------------------");
        }
    }
}
