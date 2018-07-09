using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Navigation;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

namespace SurvivalBox.ViewModels
{
    public class MainHomeViewModel : BindableBase
    {
        #region Fields

        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _dialogService;

        public DelegateCommand ControlSessionCommand { get; set; }
        public DelegateCommand AddWarningCommand { get; set; }
        public DelegateCommand RequestHelpCommand { get; set; }

        private string _sessionStatus = "START";
        public string SessionStatus
        {
            get => _sessionStatus;
            set => SetProperty(ref _sessionStatus, value);
        }

        private string _sessionInfoLabelBig = "Start a new session!";
        public string SessionInfoLabelBig
        {
            get => _sessionInfoLabelBig;
            set => SetProperty(ref _sessionInfoLabelBig, value);
        }

        private string _sessionInfoLabelSmall;
        public string SessionInfoLabelSmall
        {
            get => _sessionInfoLabelSmall;
            set => SetProperty(ref _sessionInfoLabelSmall, value);
        }

        private ObservableCollection<Session> _oldSessions;
        public ObservableCollection<Session> OldSessions
        {
            get => _oldSessions;
            set => SetProperty(ref _oldSessions, value);
        }

        private Color _sessionBackgroundColor = Color.MediumSeaGreen;
        public Color SessionBackgroundColor
        {
            get => _sessionBackgroundColor;
            set => SetProperty(ref _sessionBackgroundColor, value);
        }

        private string _infoTitleText = "IMPORTANT!";
        public string InfoTitleText
        {
            get => _infoTitleText;
            set => SetProperty(ref _infoTitleText, value);
        }

        private string _infoBodyText = "Hello I'm important!";
        public string InfoBodyText
        {
            get => _infoBodyText;
            set => SetProperty(ref _infoBodyText, value);
        }

        private bool _warningIsVisible = false;
        public bool WarningIsVisible
        {
            get => _warningIsVisible;
            set => SetProperty(ref _warningIsVisible, value);
        }

        private bool _timerIsVisible = false;
        public bool TimerIsVisible
        {
            get => _timerIsVisible;
            set => SetProperty(ref _timerIsVisible, value);
        }

        #endregion

        public MainHomeViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;

            AddWarningCommand = new DelegateCommand(AddWarning);
            ControlSessionCommand = new DelegateCommand(ControlSession);
            RequestHelpCommand = new DelegateCommand(RequestHelp);

            LoadCurSession();
            LoadOldSessions();
        }

        // HACK
        private void AddWarning()
        {
            WarningManager.Instance.AddWarning(new Warning(WarningTypes.DEHYDRATION, "Title", "Warning sample message! Lorem Ipsum blabla bla.", 1));
        }

        private async void ControlSession()
        {
            if (SessionManager.Instance.CurSession == null)
            {
                // Start new
                await _navigationService.NavigateAsync("CreateSession");
            }
            else
            {
                // End
                SessionStatus = "START";
                SessionBackgroundColor = Color.MediumSeaGreen;
                SessionInfoLabelBig = "Start a new session!";
                TimerIsVisible = false;

                SessionManager.Instance.CurSession.UpdatedTimer -= On_TimerChanged;
                await SessionManager.Instance.EndSession();
                LoadOldSessions();
            }
        }

        private void RequestHelp()
        {

        }

        private void LoadCurSession()
        {
            if (SessionManager.Instance.CurSession != null)
            {
                SessionManager.Instance.CurSession.UpdatedTimer += On_TimerChanged;
                SessionInfoLabelBig = SessionManager.Instance.CurSession.Name;
                TimerIsVisible = true;
                SessionInfoLabelSmall = SessionManager.Instance.CurSession.GetCurDuration();
                SessionStatus = "END";
                SessionBackgroundColor = Color.IndianRed;
            }
            else
            {
                // No session to load
            }
        }

        private void On_TimerChanged(Session sender, string time)
        {
            TimerIsVisible = true;
            SessionInfoLabelSmall = time;
        }

        private async void LoadOldSessions()
        {
            var items = await SessionManager.Instance.GetSessionsAsync(true, true);
            items = new ObservableCollection<Session>(items.Reverse());
            OldSessions = items;
        }
    }
}



