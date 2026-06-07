namespace SuperSudoku.Views;

public partial class HelpView : ContentPage
{
    private readonly App _app;

    public HelpView(App app)
	{
		InitializeComponent();
        _app = (App)Application.Current!;
    }

    private async void ButtonHome_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void ButtonVisitDosuku_Clicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("https://sudoku-api.vercel.app/");
    }

    private async void ButtonVisitGithub_Clicked(object sender, EventArgs e)
    {
        await Launcher.OpenAsync("https://github.com/Seraphania/SuperSudoku");
    }
}