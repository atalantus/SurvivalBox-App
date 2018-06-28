using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using SurvivalBox.Models;
using SurvivalBox.Services;

namespace SurvivalBox.ViewModels
{
	public class MainHomeViewModel : BindableBase
	{
        public DelegateCommand AddWarningCommand { get; set; }

        public MainHomeViewModel()
        {
            AddWarningCommand = new DelegateCommand(AddWarning);

            var connection = ServerConnection.DefaultConnection;
        }

	    private void AddWarning()
	    {
            WarningManager.Instance.AddWarning(new Warning("Title", "Warning sample message! Lorem Ipsum blabla bla.", 1));
	    }
    }
}
