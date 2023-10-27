using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace BonchMarkBlazor.Platforms.Android
{
    class CustomShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        private readonly IShellContext shellContext;

        public CustomShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {
            this.shellContext = shellContext;
        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            base.SetAppearance(bottomView, appearance);


            // the key is to set like below
            bottomView.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityUnlabeled;


        }
    }
}
