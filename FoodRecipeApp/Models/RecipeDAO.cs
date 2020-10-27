using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FoodRecipeApp.Models
{
    class RecipeDAO
    {
        public static List<Recipe> GetAll()
        {
            List<Recipe> result = Select("select * from recipe");
            return result;
        }

        public static List<Recipe> Select(string query)
        {
            List<Recipe> result = new List<Recipe>();
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Recipe recipe = Parse(reader);
                    result.Add(recipe);
                }
            }
            foreach(Recipe recipe in result) {
                recipe.Directions = DirectionDAO.SelectByRecipeID(recipe.ID);
                recipe.Ingredients = IngredientDAO.SelectByRecipeID(recipe.ID);
            }
            return result;
        }

        public static Recipe Parse(MySqlDataReader reader)
        {
            Recipe recipe = new Recipe();
            recipe.ID = reader.GetInt32(0);
            recipe.Name = reader.GetString(1);
            recipe.Description = reader.GetString(2);
            recipe.IsFavorite = reader.GetBoolean(3);
            return recipe;
        }

        public static int Insert(Recipe recipe)
        {
            int result = 0;
            int id = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "insert into recipe(name, description, isfavorite) values (@name, @description, @isfavorite)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@name", MySqlDbType.String).Value = recipe.Name;
                command.Parameters.Add("@description", MySqlDbType.String).Value = recipe.Description;
                command.Parameters.Add("@isfavorite", MySqlDbType.Byte).Value = recipe.IsFavorite;
                Debug.WriteLine(recipe.Name.Length);
                Debug.WriteLine(recipe.Description.Length);
                try
                {
                    result = command.ExecuteNonQuery();
                    id = (int)command.LastInsertedId;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }

            Insert(id, recipe.Directions, recipe.Ingredients);
            return result;
        }

        private static void Insert(int id, List<Direction> directions, List<Ingredient> ingredients) {
            if (id != 0)
            {
                foreach (Direction direction in directions)
                {
                    direction.RecipeID = id;
                    DirectionDAO.Insert(direction);
                }
                foreach (Ingredient ingredient in ingredients)
                {
                    ingredient.RecipeID = id;
                    IngredientDAO.Insert(ingredient);
                }
            }
        }

        public static int Delete(int id)
        {
            int result = 0;

            DirectionDAO.DeleteByRecipeID(id);
            IngredientDAO.DeleteByRecipeID(id);

            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "delete from recipe where id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

                try
                {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                }
            }
            return result;
        }

        public static int Update(Recipe recipe)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "update recipe "
                    + @" name = @name, description = @description, isfavorite = @isfavorite"
                    + @" where id = @id ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@name", MySqlDbType.String).Value = recipe.Name;
                command.Parameters.Add("@description", MySqlDbType.String).Value = recipe.Description;
                command.Parameters.Add("@isfavorite", MySqlDbType.Byte).Value = recipe.IsFavorite;

                try
                {
                    result = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Debug.WriteLine(e.StackTrace);
                }
            }
            foreach (Direction direction in recipe.Directions)
            {
                DirectionDAO.Update(direction);
            }
            foreach (Ingredient ingredient in recipe.Ingredients)
            {
                IngredientDAO.Update(ingredient);
            }
            return result;
        }
    }
}
