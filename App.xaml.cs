using SuperSudoku.Services;

namespace SuperSudoku
{
    public partial class App : Application
    {
        public SettingsService SettingsService { get; set; }
        public PuzzleService PuzzleService { get; set; }

        public App()
        {
            SettingsService = new SettingsService();
            PuzzleService = new PuzzleService();
            PuzzleService.LoadOrRequestPuzzleBox();

            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }

        protected override async void OnStart() 
        {
            await PuzzleService.LoadOrRequestPuzzleBox();
        }
    }
}