using System;
using System.Threading.Tasks;
using SurvivalBox.ViewModels;

namespace SurvivalBox.Models
{
    public class ActivityIndicatorScope : IDisposable
    {
        private bool _showIndicator;
        private ActivityIndicatorViewModelBase _viewModel;
        private Task _indicatorDelay;

        public ActivityIndicatorScope(ActivityIndicatorViewModelBase viewModel, bool showIndicator)
        {
            _viewModel = viewModel;
            this._showIndicator = showIndicator;

            if (showIndicator)
            {
                _indicatorDelay = Task.Delay(2000);
                SetIndicatorActivity(true);
            }
            else
            {
                _indicatorDelay = Task.FromResult(0);
            }
        }

        private void SetIndicatorActivity(bool isActive)
        {
            _viewModel.ActivityIndicatorIsActive = isActive;
        }

        public void Dispose()
        {
            if (_showIndicator)
            {
                _indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}