using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SurvivalBox.Models;

namespace SurvivalBox.ViewModels
{
	public class MainMasterViewModel : BindableBase
	{
        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public MainMasterViewModel()
        {
            MenuItems = new ObservableCollection<MenuItem>(new[]
            {
                new MenuItem { Id = 0, Title = "Page 1" },
                new MenuItem { Id = 1, Title = "Page 2" },
                new MenuItem { Id = 2, Title = "Page 3" },
                new MenuItem { Id = 3, Title = "Page 4" },
                new MenuItem { Id = 4, Title = "Page 5" }
            });
        }
	}
}
