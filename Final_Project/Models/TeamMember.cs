namespace Final_Project.Models

{
    public class TeamMember
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string CollegeProgram { get; set; }
        public string YearInProgram { get; set; }
        public string FavoriteLanguage { get; set; } // Example extra column
    }
}
