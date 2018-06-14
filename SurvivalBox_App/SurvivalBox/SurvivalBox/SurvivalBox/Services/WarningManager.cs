using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SurvivalBox.Models
{
    public delegate void WarningsChangedEventHandler(WarningManager sender);

    public class WarningManager
    {
        private static WarningManager _instance;
        /// <summary>
        /// The Singleton Instance
        /// </summary>
        public static WarningManager Instance => _instance ?? (_instance = new WarningManager());

        private WarningManager()
        {
            Warnings = new ObservableCollection<Warning>();
        }

        public ObservableCollection<Warning> Warnings { get; }

        public event WarningsChangedEventHandler WarningsChanged;

        public void AddWarning(Warning newWarning)
        {
            try
            {
                Warnings.Add(newWarning);
                WarningsChanged?.Invoke(this);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public void RemoveWarning(Warning warning)
        {
            try
            {
                Warnings.Remove(warning);
                WarningsChanged?.Invoke(this);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}