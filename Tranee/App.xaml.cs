namespace Tranee
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();

            // Resolve MainPage from DI (injected), host it in a NavigationPage so navigation works
            MainPage = new NavigationPage(mainPage);

        }

     /*   protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window( new NavigationPage (new MainPage()) );
        } */
    }
}