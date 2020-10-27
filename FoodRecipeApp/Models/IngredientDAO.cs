using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FoodRecipeApp.Models
{
    class IngredientDAO
    {

        public static List<Ingredient> SelectByRecipeID(int recipeid)
        {
            return Select("select * from ingredient where recipeid = " + recipeid);
        }
        public static List<Ingredient> DeleteByRecipeID(int recipeid)
        {
            return Select("delete from ingredient where recipeid = " + recipeid);
        }

        public static List<Ingredient> GetAll()
        {
            return Select("select * from ingredient");
        }

        public static List<Ingredient> Select(string query)
        {
            List<Ingredient> result = new List<Ingredient>();
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ingredient o = Parse(reader);
                    result.Add(o);
                }
            }
            return result;
        }

        public static Ingredient Parse(MySqlDataReader reader)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.ID = reader.GetInt32(0);
            ingredient.RecipeID = reader.GetInt32(1);
            ingredient.Data = reader.GetString(2);
            return ingredient;
        }

        public static int Insert(Ingredient ingredient)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "insert into ingredient(recipeid, data) values (@recipeid, @data)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@recipeid", MySqlDbType.Int32).Value = ingredient.RecipeID;
                command.Parameters.Add("@data", MySqlDbType.String).Value = ingredient.Data;

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

        public static int Delete(int id)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "delete from ingredient where id = @id";
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

        public static int Update(Ingredient ingredient)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "update ingredient "
                    + @" set recipeid = @recipeid, data = @data"
                    + @" where id = @id ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = ingredient.ID;
                command.Parameters.Add("@recipeid", MySqlDbType.Int32).Value = ingredient.RecipeID;
                command.Parameters.Add("@data", MySqlDbType.String).Value = ingredient.Data;

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
            return result;
        }
    }
}
