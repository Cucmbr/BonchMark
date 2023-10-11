using System.Text.Json;
using System.Text;

namespace BonchMark
{
    class Program
    {
        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://lk.sut.ru/cabinet/?login=yes")
        };

        static async Task PostAsync(HttpClient client)
        {
            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    userId = 77,
                    id = 1,
                    title = "write code sample",
                    completed = false
                }),
                Encoding.UTF8,
                "application/json");
        }

        static async Task Main()
        {

        }

    }
}