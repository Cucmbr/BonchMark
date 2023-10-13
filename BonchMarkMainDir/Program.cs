namespace BonchMark
{
    class Program
    {
        static async Task Main(string[] args)
        {
            BonchAPI api = new BonchAPI();
            await api.Test();
        }
    }
}
