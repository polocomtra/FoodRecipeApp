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
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    /// 
    public partial class UserControlHome : UserControl
    {
        List<Recipe> data = new List<Recipe>();        
        public UserControlHome()
        {
            InitializeComponent();
            data = RecipeDAO.GetAll();
            recipeList.ItemsSource = data;
        }

        private void FavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            //var rowItem = (sender as System.Windows.Controls.Primitives.ToggleButton).DataContext as Recipe;
            //int index1 = rowItem.ID;
            //Debug.WriteLine(rowItem);
            RecipeDAO.Save(data);
        }

        private void recipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = recipeList.SelectedIndex;
            Debug.WriteLine(index);
            //Di chuyen toi trang Detail o day

        }
    }
}
