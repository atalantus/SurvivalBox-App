using Prism.Commands;
using Prism.Mvvm;

namespace SurvivalBox.ViewModels
{
    public abstract class ActivityIndicatorViewModelBase : BindableBase
    {
        DelegateCommand RefreshingCommand { get; set; }

        public ActivityIndicatorViewModelBase()
        {
            RefreshingCommand = new DelegateCommand(OnRefresh);
        }

        private bool _activityIndicatorIsActive;
        public bool ActivityIndicatorIsActive
        {
            get => _activityIndicatorIsActive;
            set => SetProperty(ref _activityIndicatorIsActive, value);
        }

        protected abstract void OnRefresh();
    }
}