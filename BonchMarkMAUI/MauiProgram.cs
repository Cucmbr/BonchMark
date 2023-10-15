namespace BonchMarkMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("FiraSans-Regular.ttf", "Fira Sans Reg");
                    fonts.AddFont("FiraSans-Bold.ttf", "Fira Sans Bold");
                });

            return builder.Build();
        }
    }
}