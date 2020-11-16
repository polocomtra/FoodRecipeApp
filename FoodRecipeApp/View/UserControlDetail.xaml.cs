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
    /// Interaction logic for UserControlDetail.xaml
    /// </summary>
    public partial class UserControlDetail : Window
    {
        public Recipe Reci { get; set; }
        public UserControlDetail()
        {
            InitializeComponent();
        }
        public UserControlDetail(Recipe recipe)
        {
            InitializeComponent();
            Reci = new Recipe();
            Reci = recipe;
            Debug.WriteLine(Reci);
            RecipeDetailShow.DataContext = Reci;
            IngredientsList.ItemsSource = Reci.Ingredients;
            DirectionsList.DataContext = Reci.Directions;
            DirectionsList1.DataContext = Reci.Directions;
            YTlink.DataContext = Reci.Directions[0];
        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (e.Delta < 0)
            {
                scrollViewer.LineRight();
            }
            else
            {
                scrollViewer.LineLeft();
            }
            e.Handled = true;
        }
    }
}
