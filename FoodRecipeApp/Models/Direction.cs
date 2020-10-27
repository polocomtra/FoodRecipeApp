
namespace FoodRecipeApp.Models
{
    class Direction
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }

        public string ImageURL { get; set; }
        public string VideoURL { get; set; }
        public string Instruction { get; set; }

        public int Step { get; set; }
        public override string ToString()
        {
            return $"Direction:ID-'{ID}' RecipeID-'{RecipeID}' Step-'{Step}' ImageURL-'{ImageURL}' VideoURL-'{VideoURL}' Instruction-'{Instruction}'";
        }
    }
}
