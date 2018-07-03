using System;

namespace SurvivalBox.Models
{
    public class Warning
    {
        public WarningTypes Type { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Level { get; set; }
        public DateTime CreationDate { get; }
        public string CreationTime => CreationDate.ToShortTimeString();

        public Warning(WarningTypes type, string title, string message, int level)
        {
            Type = type;
            Title = title;
            Message = message;
            Level = level;
            CreationDate = DateTime.Now;
        }
    }
}