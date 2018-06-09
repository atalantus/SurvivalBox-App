using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SurvivalBox.Models
{
    public class WarningManager
    {
        private static WarningManager _instance;
        /// <summary>
        /// The Singleton Instance
        /// </summary>
        public static WarningManager Instance => _instance ?? (_instance = new WarningManager());

        private WarningManager()
        {
            Warnings = new ObservableCollection<Warning>()
            {
                new Warning("Title01", "Warning sample message! Lorem Ipsum blabla bla.", 5),
                new Warning("Title02", "Warning sample message! Lorem Ipsum blabla bla.", 5),
                new Warning("Title03", "Warning sample message! Lorem Ipsum blabla bla.", 5),
                new Warning("Title04", "Warning sample message! Lorem Ipsum blabla bla.", 5),
                new Warning("Title05", "Warning sample message! Lorem Ipsum blabla bla.", 5)
            };
        }

        public ObservableCollection<Warning> Warnings { get; }
    }
}