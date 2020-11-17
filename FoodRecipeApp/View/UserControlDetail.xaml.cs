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
        private int carouselIndex = 0;

        public UserControlDetail()
        {
            InitializeComponent();
        }
        public UserControlDetail(Recipe recipe)
        {
            InitializeComponent();
            Reci = recipe;
            Debug.WriteLine(Reci);
            Carousel.Visibility = Visibility.Hidden;
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

        private void CarouselPrevBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCarousel(carouselIndex - 1);
        }

        private void CarouselNextBtn_Click(object sender, RoutedEventArgs e)
        {
            ShowCarousel(carouselIndex + 1);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (image != null)
            {
                var dir = image.DataContext as Direction;
                if (dir != null)
                {
                    var index = Reci.Directions.IndexOf(dir);
                    if (index >= 0)
                    {
                        ShowCarousel(index);
                    }
                }
            }
        }
        private void ShowCarousel(int index)
        {
            if (index >= 0 && index < Reci.Directions.Count)
            {
                carouselIndex = index;
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                string absolutePath = $"{folder}{Reci.Directions[index].ImageName}";
                var image = new BitmapImage(new Uri(absolutePath, UriKind.Absolute));
                CarouselImage.Source = image;
                CarouselPrevBtn.Visibility = index == 0 ? Visibility.Hidden : Visibility.Visible;
                CarouselNextBtn.Visibility = index == (Reci.Directions.Count - 1) ? Visibility.Hidden : Visibility.Visible;
                Carousel.Visibility = Visibility.Visible;
            }
        }

        private void CarouselCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Carousel.Visibility = Visibility.Hidden;
        }
    }
}
