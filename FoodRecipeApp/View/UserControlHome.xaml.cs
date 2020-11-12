﻿using FoodRecipeApp.Models;
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
        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<Recipe> recipes = new ObservableCollection<Recipe>();
        int currentPageIndex = 0;
        int itemPerPage = 6;
        int totalPage = 0;
        public UserControlHome()
        {
            InitializeComponent();
            data = RecipeDAO.GetAll();
            int itemCount = data.Count;
            for(int i = 0; i < itemCount; i++)
            {
                recipes.Add(data[i]);
            }
            totalPage = itemCount / itemPerPage;
            if (itemCount % itemPerPage != 0)
            {
                totalPage++;
            }
            view.Source = recipes;
            view.Filter += new FilterEventHandler(view_Filter);
            this.recipeList.DataContext = view;
            ShowCurrentPageIndex();
            this.tbTotalPage.Text = totalPage.ToString();
            //recipeList.ItemsSource = data;
        }

        private void view_Filter(object sender, FilterEventArgs e)
        {
            int index = recipes.IndexOf((Recipe)e.Item);

            if (index >= itemPerPage * currentPageIndex && index < itemPerPage * (currentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void ShowCurrentPageIndex()
        {
            this.currentPage.Text = (currentPageIndex + 1).ToString();
        }

        private void FavoriteBtn_Click(object sender, RoutedEventArgs e)
        {
            RecipeDAO.Save(data);
        } 

        private void recipeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = recipeList.SelectedIndex;
            Debug.WriteLine(index);
            Recipe r = data[index];
            var detail = new UserControlDetail(r);
            detail.Show();
            detail.Topmost = true;
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
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex < totalPage - 1)
            {
                currentPageIndex++;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }

        private void lastBtn_Click(object sender, RoutedEventArgs e)
        {
            if (currentPageIndex != totalPage - 1)
            {
                currentPageIndex = totalPage - 1;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }
    }
}
