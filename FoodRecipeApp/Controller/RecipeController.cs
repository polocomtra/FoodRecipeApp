using FoodRecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodRecipeApp.Controller
{
    class RecipeController
    {
        public List<Recipe> data;
        public RecipeController()
        {
            data = RecipeDAO.GetAll();
        }

        public void getData()
        {
            foreach(var i in data)
            {
                Debug.WriteLine(i);
            }
        }
    }
}
