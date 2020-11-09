
namespace FoodRecipeApp.Models
{
    public class Direction
    {
        public string ImageName { get; set; } = "";
        public string VideoURL { get; set; } = "";
        public string Instruction { get; set; } = "";

        public override string ToString()
        {
            return $"Direction: ImageName-'{ImageName}' VideoURL-'{VideoURL}' Instruction-'{Instruction}'";
        }
    }
}
