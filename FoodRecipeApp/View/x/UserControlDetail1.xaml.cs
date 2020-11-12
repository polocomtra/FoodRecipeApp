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
    public partial class UserControlDetail : Page
    {
        public Recipe Recipe { get; set; }
        public UserControlDetail()
        {
            InitializeComponent();
        }

        public UserControlDetail(Recipe recipe)
        {
            InitializeComponent();
            Recipe repde = recipe;
            Debug.WriteLine(repde);
            this.Recipe = repde;
            this.RecipeDetail.DataContext = repde;
        }

    
    }
}
