namespace BonchMarkBlazor;

public partial class AppShell : Shell
{
	public static TabBar tabBar;
	public AppShell()
	{
		InitializeComponent();
		tabBar = ShellTabBar;
    }
}