using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace FoodRecipeApp.Models
{
    public class Recipe
    {
        public int ID { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }

        public BitmapImage Image { get; set; }

        public List<Direction> Directions;
        public List<string> Ingredients;

        public override string ToString()
        {
            return $"ID-'{ID}' Name-'{Name}' Image-'{Image}' IsFav-{IsFavorite}";
        }
    }
}
