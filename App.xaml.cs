using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class App : Application
    {
        public SettingsService SettingsService { get; set; }
        public PuzzleService PuzzleService { get; set; }

        public App()
        {
			InitializeComponent();

			SettingsService = new SettingsService();
            PuzzleService = new PuzzleService();
            _ = PuzzleService.InitialiseAsync();

            Directory.CreateDirectory(AppPaths.AppDirectory);            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}