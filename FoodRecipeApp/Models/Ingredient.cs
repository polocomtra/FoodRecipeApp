
namespace FoodRecipeApp.Models
{
    class Ingredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string Data { get; set; }

        public override string ToString()
        {
            return $"Ingredient:ID-'{ID}' RecipeID-'{RecipeID}' Data-'{Data}'";
        }
    }
}
