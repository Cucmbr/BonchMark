namespace BonchMark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BonchAPI api = new BonchAPI();
            api.Test();
        }
    }
}