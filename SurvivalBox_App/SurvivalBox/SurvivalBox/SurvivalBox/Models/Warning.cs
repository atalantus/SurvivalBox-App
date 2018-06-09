using System;

namespace SurvivalBox.Models
{
    public class Warning
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public int Level { get; set; }
        public DateTime CreationDate { get; }
        public string CreationTime => CreationDate.ToShortTimeString();

        public Warning(string title, string message, int level)
        {
            Title = title;
            Message = message;
            Level = level;
            CreationDate = DateTime.Now;
        }
    }
}