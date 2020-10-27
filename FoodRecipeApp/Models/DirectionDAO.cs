using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace FoodRecipeApp.Models
{
    class DirectionDAO
    {
        public static List<Direction> SelectByRecipeID(int recipeid)
        {
            return Select("select * from direction where recipeid = " + recipeid);
        }
        public static List<Direction> DeleteByRecipeID(int recipeid)
        {
            return Select("delete from direction where recipeid = " + recipeid);
        }

        public static List<Direction> GetAll()
        {
            return Select("select * from direction");
        }

        public static List<Direction> Select(string query)
        {
            List<Direction> result = new List<Direction>();
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Direction o = Parse(reader);
                    result.Add(o);
                }
            }
            return result;
        }
        
        public static Direction Parse(MySqlDataReader reader)
        {
            Direction direction = new Direction();
            direction.ID = reader.GetInt32(0);
            direction.RecipeID = reader.GetInt32(1);
            direction.ImageURL = reader.GetString(2);
            direction.VideoURL = reader.GetString(3);
            direction.Instruction = reader.GetString(4);
            direction.Step = reader.GetInt32(5);
            return direction;
        }

        public static int Insert(Direction direction)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "insert into direction(recipeid, imageurl, videourl, instruction, step) values (@recipeid, @imageurl, @videourl, @instruction, @step)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@recipeid", MySqlDbType.Int32).Value = direction.RecipeID;
                command.Parameters.Add("@imageurl", MySqlDbType.String).Value = direction.ImageURL;
                command.Parameters.Add("@videourl", MySqlDbType.String).Value = direction.VideoURL;
                command.Parameters.Add("@instruction", MySqlDbType.String).Value = direction.Instruction;
                command.Parameters.Add("@step", MySqlDbType.Int32).Value = direction.Step;

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

        public static int Delete(string query)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                //string query = "delete from direction where id = @id";
                MySqlCommand command = new MySqlCommand(query, connection);
                //command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

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

        public static int Update(Direction direction)
        {
            int result = 0;
            using (MySqlConnection connection = new MySqlConnection(DBConnector.ConnectionString))
            {
                connection.Open();
                string query = "update direction "
                    + @" set recipeid = @recipeid, imageurl = @imageurl, videourl = @videourl, instruction = @instruction, step = @step"
                    + @" where id = @id ";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.Add("@id", MySqlDbType.Int32).Value = direction.ID;
                command.Parameters.Add("@recipeid", MySqlDbType.Int32).Value = direction.RecipeID;
                command.Parameters.Add("@imageurl", MySqlDbType.String).Value = direction.ImageURL;
                command.Parameters.Add("@videourl", MySqlDbType.String).Value = direction.VideoURL;
                command.Parameters.Add("@instruction", MySqlDbType.String).Value = direction.Instruction;
                command.Parameters.Add("@step", MySqlDbType.Int32).Value = direction.Step;

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
