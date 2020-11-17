using FoodRecipeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoodRecipeApp.View
{
    /// <summary>
    /// Interaction logic for UserControlFavoriteList.xaml
    /// </summary>
    public partial class UserControlFavoriteList : UserControl
    {
        List<Recipe> data = new List<Recipe>();
        public UserControlFavoriteList()
        {
            InitializeComponent();
            data = RecipeDAO.GetAll();
            recipeList.ItemsSource = data;
        }
        private void FavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            RecipeDAO.Save(data);
        }

        private void recipeList_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            var index = recipeList.SelectedIndex;
            if (index >= 0 && index < data.Count)
            {
                Recipe r = data[index];
                var detail = new UserControlDetail(r);
                detail.Show();
                detail.Topmost = true;
            }
        }

    }
}
