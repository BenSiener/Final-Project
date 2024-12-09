namespace Final_Project.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DifficultyLevel { get; set; } // Scale 1-10
        public string PreferredTime { get; set; } // Morning, Afternoon, etc.
        public double Cost { get; set; }
    }
}
