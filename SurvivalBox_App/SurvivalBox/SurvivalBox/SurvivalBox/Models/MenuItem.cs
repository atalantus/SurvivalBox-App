using System;
using SurvivalBox.Views;

namespace SurvivalBox.Models
{
    public class MenuItem
    {
        public MenuItem()
        {
            TargetType = typeof(MainDetail01);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}