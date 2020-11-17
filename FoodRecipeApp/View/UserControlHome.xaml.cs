using FoodRecipeApp.Converter;
using FoodRecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        List<Recipe> recipesView = new List<Recipe>();
        //CollectionViewSource view = new CollectionViewSource();
        //ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();
        int currentPageIndex = 0;
        int itemPerPage = 4;
        int totalPage = 0;
        public UserControlHome()
        {
            InitializeComponent();
            data = RecipeDAO.GetAll();
            caclPages(data.Count);
            //view.Source = recipes;
            //view.Filter += new FilterEventHandler(view_Filter);
            //this.recipeList.DataContext = view;
            //ShowCurrentPageIndex();
            this.tbTotalPage.Text = totalPage.ToString();
            //recipeList.ItemsSource = data;
            
            displayRecipes();
            //Debug.WriteLine(currentPageIndex);
            //Debug.WriteLine(recipesView.Count);
        }

        //private void view_Filter(object sender, FilterEventArgs e)
        //{
        //    int index = recipes.IndexOf((Recipe)e.Item);

        //    if (index >= itemPerPage * currentPageIndex && index < itemPerPage * (currentPageIndex + 1))
        //    {
        //        e.Accepted = true;
        //    }
        //    else
        //    {
        //        e.Accepted = false;
        //    }
        //}

        void caclPages(int itemCount)
        {
            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage++;
            }
        }

        void displayRecipes()
        {
            var key = VNCharacterUtils.RemoveAccent(keywordTextBox.Text.Trim().ToLower());
            var skip = currentPageIndex * itemPerPage;
            var take = itemPerPage;
            var query = from p in data where IsMatch(p.Name, key) select p;
            caclPages(query.Count());
            recipesView = query.Skip(skip).Take(take).ToList();
            recipeList.ItemsSource = recipesView;

            ShowCurrentPageIndex();
        }

        private void ShowCurrentPageIndex()
        {
            this.currentPage.Text = totalPage == 0 ? "0" : (currentPageIndex + 1).ToString();
        }

        private void recipeList_SelectionChanged(object sender, MouseButtonEventArgs e)
        {
            var index = recipeList.SelectedIndex;
            if (index >= 0 && index < recipesView.Count)
            {
                Recipe r = recipesView[index];
                var detail = new UserControlDetail(r);
                detail.Show();
                detail.Topmost = true;
            }
        }

        private void FavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            RecipeDAO.Save(data);
        } 

        private void keywordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keywordPlaceholderTextBlock.Visibility = Visibility.Hidden;
        }

        private void keywordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (keywordPlaceholderTextBlock.Text.Length == 0)
            {
                keywordPlaceholderTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void firstBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex != 0)
            {
                currentPageIndex = 0;
                //view.View.Refresh();
                displayRecipes();
                
            }
            //ShowCurrentPageIndex();
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(currentPageIndex);
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                //view.View.Refresh();
                displayRecipes();
            }
            //ShowCurrentPageIndex();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (currentPageIndex < totalPage - 1)
            {
                currentPageIndex++;
                //view.View.Refresh();
                displayRecipes();
            }
            //ShowCurrentPageIndex();
        }

        private void lastBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex != totalPage - 1)
            {
                currentPageIndex = totalPage - 1;
                //view.View.Refresh();
                displayRecipes();
            }
            //ShowCurrentPageIndex();
        }

        private async void KeywordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string oldKey = keywordTextBox.Text;
            await Task.Delay(250);
            if (!oldKey.Equals(keywordTextBox.Text))
            {
                Search(keywordTextBox.Text);
            }
        }

        private void Search(string key)
        {
            currentPageIndex = 0;
            displayRecipes();
        }

        private bool IsMatch(string name, string key)
        {
            var result = VNCharacterUtils.RemoveAccent(name.ToLower()).Contains(key);
            return result;
        }

        private void recipeList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel panel = sender as StackPanel;
            if (panel != null)
            {
                Recipe recipe = panel.DataContext as Recipe;
                if (recipe != null)
                {
                    var detail = new UserControlDetail(recipe);
                    detail.Show();
                    detail.Topmost = true;
                }
            }
        }

    }
}
