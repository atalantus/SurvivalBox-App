namespace SurvivalBox.Models
{
    public class User
    {
        private static User _instance;

        public static User Instance => _instance ?? (_instance = new User());

        private User() { }

        /// <summary>
        /// Name of the user
        /// </summary>
        public string Name { get; set; }
    }
}