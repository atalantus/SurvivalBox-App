using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Prism.Services;
using SurvivalBox.Models;

namespace SurvivalBox.ViewModels
{
	public class MainWarningsViewModel : BindableBase
	{
	    private IPageDialogService _dialogService;

	    private ObservableCollection<Warning> _warnings;

	    public ObservableCollection<Warning> Warnings
	    {
	        get => _warnings;
	        set => SetProperty(ref _warnings, value);
	    }

	    public MainWarningsViewModel(IPageDialogService dialogService)
	    {
	        _dialogService = dialogService;

	        _warnings = new ObservableCollection<Warning>()
	        {
	            new Warning("Title01", "Warning sample message! Lorem Ipsum blabla bla.", 5),
	            new Warning("Title02", "Warning sample message! Lorem Ipsum blabla bla.", 5),
	            new Warning("Title03", "Warning sample message! Lorem Ipsum blabla bla.", 5),
	            new Warning("Title04", "Warning sample message! Lorem Ipsum blabla bla.", 5),
	            new Warning("Title05", "Warning sample message! Lorem Ipsum blabla bla.", 5),
	        };
	    }
    }
}
