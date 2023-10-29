namespace BonchMarkBlazor
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        internal async void ToTimetable()
        {
            await Navigation.PushAsync(new TimetablePage());
        }
    }
}
