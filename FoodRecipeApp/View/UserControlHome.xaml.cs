using FoodRecipeApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    class User
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
    public partial class UserControlHome : UserControl
    {
        BindingList<User> data = new BindingList<User>();
        public UserControlHome()
        {
            InitializeComponent();
            data.Add(new User() { Name = "tai", Image = "../Images/mot1.jpg" });
            data.Add(new User() { Name = "tien", Image = "../Images/mot2.jpg" });
            recipeList.ItemsSource = data;
        }
    }
}
