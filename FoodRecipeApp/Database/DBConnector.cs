using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace FoodRecipeApp.Models
{
    
    public class DBConnector
    {
        public static string ConnectionString = "Server=localhost; database=dbFoodRecipeApp; UID=root; password=password";
        public static int CreateDatabase()
        {
            int result = 0;
            string connstr = "Server=localhost; UID=root; password=password";
            using (MySqlConnection connection = new MySqlConnection(connstr)) {
                connection.Open();

                string query = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "Database/csdl.sql");
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                } 
            }
            InsertValues();
            return result;
        }
        private static void InsertValues()
        {
            string[] images = { "tra_sua_chai_latte.jpg", "sinh_to_baobab.jpg", "tom_chien.jpg", "canh_kim_chi.jpg" };
            StreamReader file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "Database/data.txt");
            string line = file.ReadLine();
            int i = 0;
            while (!string.IsNullOrEmpty(line))
            {
                var name = file.ReadLine();
                var des = file.ReadLine();
                var isFav = file.ReadLine() == "0" ? false : true;
                file.ReadLine();
                Recipe recipe = new Recipe();

                recipe.Name = name;
                recipe.Description = des;
                recipe.IsFavorite = isFav;
                recipe.Directions = new List<Direction>();
                recipe.Ingredients = new List<Ingredient>();

                var ing = "";
                while ((ing = file.ReadLine()) != "&")
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.Data = ing;
                    recipe.Ingredients.Add(ingredient);
                }

                var dir = "";
                while ((dir = file.ReadLine()) != "&&")
                {
                    Direction direction = new Direction();
                    direction.ImageURL = "Images/" + images[i];
                    direction.VideoURL = "null";
                    direction.Step = recipe.Directions.Count + 1;
                    direction.Instruction = dir;
                    recipe.Directions.Add(direction);
                }
                RecipeDAO.Insert(recipe);
                i++;
                line = file.ReadLine();
            }

        }
    }
}
