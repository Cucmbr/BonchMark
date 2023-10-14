using System;
using System.IO;
using Xamarin.Forms;


namespace BonchMarkApp
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnButtonLogin(object sender, EventArgs args)
        {
            if()
            if (App.files.Length != 1)
            {
                File.WriteAllText(Path.Combine(App.folderPath, "UserData.txt"), users)
            }
        }
    }
}