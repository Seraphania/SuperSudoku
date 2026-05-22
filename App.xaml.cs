using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class App : Application
    {
        public SettingsService SettingsService { get; set; }
        public PuzzleService PuzzleService { get; set; }
        public GameService GameService { get; set; }

        public App()
        {
			InitializeComponent();

			Directory.CreateDirectory(AppPaths.AppDirectory);
			SettingsService = new SettingsService();
            PuzzleService = new PuzzleService();
			_ = PuzzleService.InitialiseAsync();
            GameService = new GameService(PuzzleService);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}