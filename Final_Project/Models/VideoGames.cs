namespace Final_Project.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; } // PC, PlayStation, Xbox, etc.
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; } // Scale 1-10
    }
}
