using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Tabs;
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

    class CustomShellTabLayoutAppearanceTracker : ShellTabLayoutAppearanceTracker
    {
        private readonly IShellContext shellContext;

        public CustomShellTabLayoutAppearanceTracker(IShellContext shellContext) : base(shellContext)
        {
        }

        public override void SetAppearance(TabLayout tabLayout, ShellAppearance appearance)
        {
            base.SetAppearance(tabLayout, appearance);
        }
    }
}
