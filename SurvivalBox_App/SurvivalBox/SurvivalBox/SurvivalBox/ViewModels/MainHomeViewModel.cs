using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Prism.Services;
using SurvivalBox.Models;
using SurvivalBox.Services;
using Xamarin.Forms;

namespace SurvivalBox.ViewModels
{
	public class MainHomeViewModel : BindableBase
	{
        #region Fields

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

	    private string _sessionInfoLabelSmall = "2:13";
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

        public MainHomeViewModel(IPageDialogService dialogService)
        {
            _dialogService = dialogService;

            AddWarningCommand = new DelegateCommand(AddWarning);
            ControlSessionCommand = new DelegateCommand(ControlSession);
            RequestHelpCommand = new DelegateCommand(RequestHelp);

            var connection = ServerConnection.DefaultConnection;

            OldSessions = new ObservableCollection<Session>()
            {
                new Session() {Name = "Mount Everest"},
                new Session() {Name = "K2"},
                new Session() {Name = "Kangchenjunga"},
                new Session() {Name = "Lhotse"}
            };
        }

	    private void AddWarning()
	    {
            WarningManager.Instance.AddWarning(new Warning(WarningTypes.DEHYDRATION, "Title", "Warning sample message! Lorem Ipsum blabla bla.", 1));
	    }

	    private async void ControlSession()
	    {
	        if (SessionManager.Instance.CurSession == null)
	        {
                // Star new
	            var name = "SessionName";
	            var newSession = new Session()
	            {
                    Name = name,
	                CurState = Session.States.ACTIVE,
	                StartDate = DateTime.UtcNow
	            };
                await SessionManager.Instance.CreateSession(newSession);
	            SessionStatus = "END";
	            SessionBackgroundColor = Color.IndianRed;
	            SessionInfoLabelBig = name;
	            TimerIsVisible = true;
	            SessionInfoLabelSmall = "00:00:00";

                CountSessionTime();
	        } else
	        {
	            // End
    
	        }
	    }

	    private void RequestHelp()
	    {

	    }

	    private void CountSessionTime()
	    {
	        var startTime = SessionManager.Instance.CurSession.StartDate;

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (SessionManager.Instance.CurSession != null)
                {
                    Debug.WriteLine($"StartTime: {startTime.Date} | Now: {DateTime.UtcNow.Date}");
                    var time = DateTime.UtcNow - startTime.ToUniversalTime();
                    SessionInfoLabelSmall = time.ToString(@"hh\:mm\:ss");
                    return true;
                }
                else
                {
                    return false;
                }
            });
	    }
    }
}
