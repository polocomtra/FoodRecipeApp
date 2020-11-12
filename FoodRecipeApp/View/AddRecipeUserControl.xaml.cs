using FoodRecipeApp.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for AddRecipeUserControl.xaml
    /// </summary>
    public partial class AddRecipeUserControl : UserControl
    {
        private Recipe recipe = new Recipe() { IsFavorite = false};
        private BindingList<string> ingredients = new BindingList<string>();
        private BindingList<Direction> directions = new BindingList<Direction>();

        public AddRecipeUserControl()
        {
            InitializeComponent();

            IngredientListView.ItemsSource = ingredients;
            DirectionListView.ItemsSource = directions;
        }

        private void RecipeImageButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string dataContext = button.DataContext as string;
                button.Opacity = string.IsNullOrEmpty(dataContext) ? 1 : 0;
            }
        }

        private void RecipeImageButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                button.Opacity = 1;
            }
        }

        private void RecipeImageButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null && button.Opacity > 0.01)
            {
                Debug.WriteLine(button.DataContext);
                var dialog = new OpenFileDialog();
                dialog.DefaultExt = ".png";
                dialog.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif"
                                    + "|PNG Portable Network Graphics (*.png)|*.png"
                                    + "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif"
                                    + "|BMP Windows Bitmap (*.bmp)|*.bmp"
                                    + "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff"
                                    + "|GIF Graphics Interchange Format (*.gif)|*.gif";
                if (true == dialog.ShowDialog())
                {
                    string imageName = System.IO.Path.GetFileName(dialog.FileName);
                    string imageRP = "Images/" + imageName;
                    string imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
                    var prefix = 0;
                    while (File.Exists(imagePath))
                    {
                        prefix += 1;
                        imageRP = "Images/i" + prefix + imageName;
                        imagePath = AppDomain.CurrentDomain.BaseDirectory + imageRP;
                    }
                    File.Copy(dialog.FileName, imagePath);
                    var parent = button.Parent as Grid;
                    if (parent != null)
                    {
                        parent.DataContext = imageRP;
                    }

                    if (parent.Equals(RecipeImageGrid))
                    {
                        recipe.Image = imageRP;
                    }
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null)
            {
                string dc = button.DataContext as string;
                if (dc != null)
                {
                    ingredients.Remove(dc);
                }
            }
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            string ingredient = IngredientTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(ingredient))
            {
                ingredients.Add(ingredient);
                IngredientTextBox.Text = "";
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            var error = ValidateRecipe();
            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(error, "FoodRecipeApp", MessageBoxButton.OK, MessageBoxImage.Error);
            } else
            {
                recipe.Name = NameTextBox.Text;
                recipe.Description = DescriptionTextBox.Text;
                recipe.Ingredients = ingredients.ToList();
                recipe.Directions = directions.ToList();
                if (RecipeDAO.Insert(recipe))
                {
                    MessageBox.Show("Đã thêm công thức", "FoodRecipeApp", MessageBoxButton.OK, MessageBoxImage.Information);
                    Reset();
                } else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại", "FoodRecipeApp", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Reset()
        {
            recipe = new Recipe();
            NameTextBox.Text = "";
            DescriptionTextBox.Text = "";
            RecipeImageGrid.DataContext = "";
            RecipeImageButton.Opacity = 1;
            ingredients.Clear();
            directions.Clear();
        }
        
        private string ValidateRecipe()
        {
            string result = null;
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                result = "Vui lòng thêm tên cho công thức";
            }
            else if (string.IsNullOrEmpty(DescriptionTextBox.Text))
            {
                result = "Vui lòng thêm mô tả cho công thức";
            }
            else if (string.IsNullOrEmpty(recipe.Image))
            {
                result = "Vui lòng chọn ảnh đại diện cho công thức";
            }
            else if (ingredients.Count == 0)
            {
                result = "Vui lòng thêm nguyên liệu cho công thức";
            }
            else if (directions.Count == 0)
            {
                result = "Vui lòng thêm các bước thực hiện cho công thức";
            }
            else if (!IsValidDirections())
            {
                result = "Vui lòng thêm cách thực hiện cho các bước";
            }
            return result;
        }

        private bool IsValidDirections()
        {
            var result = true;
            foreach (var d in directions)
            {
                if (string.IsNullOrEmpty(d.Instruction))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void DirRemoveButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DirectionListView.SelectedIndex;
            
            if (selectedIndex >=0 && selectedIndex < directions.Count) {
                directions.RemoveAt(selectedIndex);
            }
        }

        private void DirAddButton_Click(object sender, RoutedEventArgs e)
        {
            directions.Add(new Direction());
        }

        private void DirDuplicateButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedIndex = DirectionListView.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < directions.Count)
            {
                directions.Add(CopyDirection(directions[selectedIndex]));
            }
        }

        private Direction CopyDirection(Direction d)
        {
            Direction result = new Direction()
            {
                ImageName = d.ImageName,
                VideoURL = d.VideoURL,
                Instruction = d.Instruction
            };
            return result;
        }
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DirectionListView.UnselectAll();
        }
    }
}
