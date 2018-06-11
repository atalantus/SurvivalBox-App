using Prism.Commands;
using Prism.Mvvm;

namespace SurvivalBox.ViewModels
{
    public abstract class ActivityIndicatorViewModelBase : BindableBase
    {
        public DelegateCommand RefreshingCommand { get; set; }

        protected ActivityIndicatorViewModelBase()
        {
            RefreshingCommand = new DelegateCommand(OnRefresh);
        }

        private bool _activityIndicatorIsActive;
        public bool ActivityIndicatorIsActive
        {
            get => _activityIndicatorIsActive;
            set => SetProperty(ref _activityIndicatorIsActive, value);
        }

        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        protected abstract void OnRefresh();
    }
}