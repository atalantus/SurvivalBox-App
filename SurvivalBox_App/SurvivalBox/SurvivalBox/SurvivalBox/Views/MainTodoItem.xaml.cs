using System;
using System.Threading.Tasks;
using SurvivalBox.Services;
using SurvivalBox.ViewModels;
using Xamarin.Forms;

namespace SurvivalBox.Views
{
    public partial class MainTodoItem : ContentPage
    {
        private MainTodoItemViewModel _vm;

        public MainTodoItem()
        {
            InitializeComponent();
            _vm = (MainTodoItemViewModel) BindingContext;
        }

        //HACK: PRISM hat momentan nicht die Moeglichkeit OnAppearing() im ViewModel zu handeln
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            await _vm.RefreshItems(true, true);
        }
    }
}
