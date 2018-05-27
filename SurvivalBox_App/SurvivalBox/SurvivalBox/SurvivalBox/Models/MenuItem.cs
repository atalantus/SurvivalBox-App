using System;
using SurvivalBox.Views;

namespace SurvivalBox.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IconSource { get; set; }

        public string ViewName { get; set; }
    }
}