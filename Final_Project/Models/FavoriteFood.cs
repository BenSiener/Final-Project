namespace Final_Project.Models
{
    public class FavoriteFood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CuisineType { get; set; }
        public bool IsVegetarian { get; set; }
        public int Calories { get; set; }
        public string FlavorProfile { get; set; } // e.g., Sweet, Spicy, Savory
    }
}

