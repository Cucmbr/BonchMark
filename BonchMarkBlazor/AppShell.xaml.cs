namespace BonchMarkBlazor;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
    }

	public void ShowTab()
	{
		HideElement.IsVisible = true;
	}
}