namespace BonchMarkMAUI;

public partial class LoginPage : ContentPage
{
	public LoginPage()
        {
            InitializeComponent();
        }

    private async void OnButtonLogin(object sender, EventArgs args)
    {
        if (!await App.api.Init())
        {
            ServerErr.IsVisible = true;
        }
        else if (!await App.api.Login(usersEntry.Text, paroleEntry.Text))
        {
            AuthErr.IsVisible = true;
        }
        else
        {
            File.WriteAllText(Path.Combine(App.folderPath, "UserData.txt"), usersEntry.Text + ":" + paroleEntry.Text);
            await Navigation.PushAsync(new NavigationPage(new MainPage()));
        }
    }

    private void PressAnim(object sender, EventArgs args)
    {
        loginButton.ScaleTo(0.95, 100, Easing.Linear);
    }
    private void ReleaseAnim(object sender, EventArgs args)
    {
        loginButton.ScaleTo(1, 100, Easing.Linear);
    }
}