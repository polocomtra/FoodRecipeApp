﻿using System.Collections.Generic;

namespace FoodRecipeApp.Models
{
    class Recipe
    {
        public int ID { get; set; }
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public List<Direction> Directions;
        public List<Ingredient> Ingredients;

        public override string ToString()
        {
            return $"Recipe: ID-'{ID}' Name-'{Name}' IsFav-{IsFavorite}";
        }
    }
}
