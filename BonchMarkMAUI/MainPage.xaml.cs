using BonchMark;
namespace BonchMarkMAUI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnButtonMark(object sender, EventArgs args)
        {
            string[] userData = File.ReadAllText(Path.Combine(App.folderPath, "UserData.txt")).Split(":");
            markButton.IsEnabled = false;
            await markButton.FadeTo(0.5, 100, Easing.Linear);
            if (await App.api.Init())
            {
                if (await App.api.Login(userData[0], userData[1]))
                {
                    BonchAPI.MarkStatus code = await App.api.MarkSequence();
                    switch (code)
                    {
                        case BonchAPI.MarkStatus.OK:
                            StatusUIlabel.Text = "Успешно ✔️";
                            StatusUI.BackgroundColor = new Color(186, 246, 140);
                            break;
                        case BonchAPI.MarkStatus.UpdateOnly:
                            StatusUI.BackgroundColor = new Color(255, 218, 150);
                            StatusUIlabel.Text = "Преподаватель ещё не начал занятие 🕰️";
                            break;
                        case BonchAPI.MarkStatus.NoButton:
                            StatusUIlabel.Text = "Занятия ещё не начались 💤";
                            StatusUI.BackgroundColor = new Color(255, 218, 150);
                            break;
                        case BonchAPI.MarkStatus.RequestFailed:
                            StatusUIlabel.Text = "Ошибка! ❌";
                            StatusUI.BackgroundColor = new Color(255, 197, 197);
                            break;
                    }
                }
                else
                {
                    StatusUIlabel.Text = "Что-то пошло не так ❓";
                    StatusUI.BackgroundColor = new Color(255, 197, 197);
                }
            }
            else
            {
                StatusUIlabel.Text = "Что-то пошло не так ❓";
                StatusUI.BackgroundColor = new Color(255, 197, 197);
            }
            StatusUI.VerticalOptions = LayoutOptions.CenterAndExpand;
            await markButton.FadeTo(1, 100, Easing.Linear);
            StatusUI.IsVisible = true;
            markButton.IsEnabled = true;
        }

        private void PressAnim(object sender, EventArgs args)
        {
            markButton.ScaleTo(0.95, 100, Easing.Linear);
        }
    
        private void ReleaseAnim(object sender, EventArgs args)
        {
            markButton.ScaleTo(1, 100, Easing.Linear);
        }
    }
}