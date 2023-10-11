using System.Text.Json;
using System.Text;

namespace BonchMark
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://lk.sut.ru/cabinet/?login=yes")
        };

        // we have to make working and convinient way of interacting with data we get from client.
        // тут точно замешано асинхронное программирование, насколько я понял TAP. Ещё в целом надо понять, все ли геты нужно повторять.
        // вот так
        //
        static async Task Main()
        {
            StringContent content = new StringContent("BENRO"); // бенро?? крутяк))
            using var response = await client.PostAsync(client.BaseAddress, content);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }

    }
}