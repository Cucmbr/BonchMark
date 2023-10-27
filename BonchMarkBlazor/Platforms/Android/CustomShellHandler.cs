using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace BonchMarkBlazor
{
    class CustomShellHandler : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new Platforms.Android.CustomShellBottomNavViewAppearanceTracker(this, shellItem.CurrentItem);
        }
    }
}
