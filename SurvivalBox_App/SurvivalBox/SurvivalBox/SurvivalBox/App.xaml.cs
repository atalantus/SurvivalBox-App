using Prism;
using Prism.Ioc;
using SurvivalBox.ViewModels;
using SurvivalBox.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SurvivalBox
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("WelcomeCarousel");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<WelcomeCarousel>();
            containerRegistry.RegisterForNavigation<RegKey>();
            containerRegistry.RegisterForNavigation<Login>();
            containerRegistry.RegisterForNavigation<CreateAccount>();
            containerRegistry.RegisterForNavigation<PasswordReset>();
            containerRegistry.RegisterForNavigation<Main>();
            containerRegistry.RegisterForNavigation<MainMaster>();
            containerRegistry.RegisterForNavigation<MainDetail01>();
        }
    }
}
