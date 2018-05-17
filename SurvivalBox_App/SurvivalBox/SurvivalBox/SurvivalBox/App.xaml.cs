using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Prism;
using Prism.Ioc;
using SurvivalBox.ViewModels;
using SurvivalBox.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using SurvivalBox.Models;
using SurvivalBox.Services;

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

            var item = new TodoItem()
            {
                Name = "Jonas schlagen"
            };

            var list01 = TodoItemManager.Instance.GetTodoItemsAsync();
            var hallo = await Test(item);
            Debug.WriteLine("hey");

            await NavigationService.NavigateAsync("WelcomeCarousel");
        }

        private async Task<IEnumerable<TodoItem>> Test(TodoItem item)
        {
            await TodoItemManager.Instance.AddItem(item);
            return await TodoItemManager.Instance.GetTodoItemsAsync();
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
