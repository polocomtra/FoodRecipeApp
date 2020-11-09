using FoodRecipeApp.View;
using System;
using System.Collections.Generic;
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

namespace FoodRecipeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            switch (index)
            {
                case 0:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UserControlHome());
                    break;
                case 1:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new UserControlFavoriteList());
                    break;
                case 2:
                    MainGrid.Children.Clear();
                    MainGrid.Children.Add(new AddRecipeUserControl());
                    break;
            }
        }
        private void MoveCursorMenu(int index)
        {
            TransitionContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (150 + (60 * index)), 0, 0);
        }
    }
}
