using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using SurvivalBox.Models;

namespace SurvivalBox.ViewModels
{
	public class MainHomeViewModel : BindableBase
	{
        public DelegateCommand AddWarningCommand { get; set; }

        public MainHomeViewModel()
        {
            AddWarningCommand = new DelegateCommand(AddWarning);
        }

	    private void AddWarning()
	    {
            WarningManager.Instance.AddWarning(new Warning("Title", "Warning sample message! Lorem Ipsum blabla bla.", 2));
	    }
    }
}
