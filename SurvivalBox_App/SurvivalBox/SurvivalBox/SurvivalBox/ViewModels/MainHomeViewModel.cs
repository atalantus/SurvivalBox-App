using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SurvivalBox.Models;
using SurvivalBox.Services;

namespace SurvivalBox.ViewModels
{
	public class MainHomeViewModel : BindableBase
	{
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

	    private string _sessionInfoLabelSmall = string.Empty;
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

        public MainHomeViewModel()
        {
            AddWarningCommand = new DelegateCommand(AddWarning);
            ControlSessionCommand = new DelegateCommand(ControlSession);
            RequestHelpCommand = new DelegateCommand(RequestHelp);

            var connection = ServerConnection.DefaultConnection;

            OldSessions = new ObservableCollection<Session>()
            {
                new Session("Alpen 1"),
                new Session("Alpen 2"),
                new Session("Himalaya 1"),
                new Session("Schleuderklamm")
            };
        }

	    private void AddWarning()
	    {
            WarningManager.Instance.AddWarning(new Warning(WarningTypes.DEHYDRATION, "Title", "Warning sample message! Lorem Ipsum blabla bla.", 1));
	    }

	    private void ControlSession()
	    {

	    }

	    private void RequestHelp()
	    {

	    }
    }
}
