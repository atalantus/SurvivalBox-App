using SurvivalBox.ViewModels;
using Xamarin.Forms;

namespace SurvivalBox.Views
{
    public partial class MainTracker : TabbedPage
    {
        public MainTracker()
        {
            InitializeComponent();
            var vm = (MainTrackerViewModel) BindingContext;
            vm.TrackerMap = TrackerMapElement;
            vm.StartTrackerMap();
        }
    }
}
