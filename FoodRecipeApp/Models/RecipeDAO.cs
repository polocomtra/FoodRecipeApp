using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace FoodRecipeApp.Models
{
    class RecipeDAO
    {
        private static string ReadFile()
        {
            string filepath = AppDomain.CurrentDomain.BaseDirectory + "data.json";
            string result = File.ReadAllText(filepath);
            return result;
        }
        public static List<Recipe> GetAll()
        {
            var result = new List<Recipe>();
            string jsonString = ReadFile();
            result = JsonConvert.DeserializeObject<List<Recipe>>(jsonString) ?? new List<Recipe>();
            return result;
        }

        public static bool Save(List<Recipe> recipe)
        {
            var result = false;
            try
            {
                string jsonString = JsonConvert.SerializeObject(recipe, Formatting.Indented);
                File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "data.json", jsonString);
                result = true;
            }catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return result;
        }
        public static bool Insert(Recipe recipe)
        {
            var result = false;
            var oldID = recipe.ID;
            var list = GetAll();
            recipe.ID = list.Count == 0 ? 0 : list[list.Count - 1].ID + 1;
            list.Add(recipe);
            result = Save(list);
            if (!result)
            {
                recipe.ID = oldID;
            }
            return result;
        }

        public static int Delete(int id)
        {
            int result = 0;
            var list = GetAll();
            result = list.RemoveAll(e => e.ID == id);
            Save(list);
            return result;
        }

        public static bool Update(Recipe recipe)
        {
            var result = false;
            var list = GetAll();
            var oldRecipeIndex = list.FindIndex(e => e.ID == recipe.ID);
            
            if (oldRecipeIndex >= 0 && oldRecipeIndex < list.Count)
            {
                list[oldRecipeIndex] = recipe;
                result = Save(list);
            }

            return result;
        }
    }
}
