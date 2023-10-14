namespace BonchMark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to BonchMark\n");
            BonchAPI api = new BonchAPI();
            Console.WriteLine(await api.MarkSequence("vanvanich531%40gmail.com", "2MFPBNG8RHB"));
        }
    }
}
