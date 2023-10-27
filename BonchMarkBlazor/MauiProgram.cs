namespace BonchMarkBlazor
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
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers =>
                {
#if ANDROID
            handlers.AddHandler(typeof(Shell), typeof(CustomShellHandler));
#endif
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            return builder.Build();
        }
    }
}