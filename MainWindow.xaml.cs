using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using OxyPlot;

namespace RecipeAppGUI
{
    public partial class MainWindow : Window
    {
        private List<RecipeApplication> RecipesList;
        private object pieChart;

        public MainWindow()
        {
            InitializeComponent();
            InitializeRecipe();
        }

        private void InitializeRecipe()
        {
            RecipesList = FetchRecipes();
            UpdateRecipes();
        }

        private void UpdateRecipes()
        {
            var filteredRecipes = RecipesList.AsEnumerable();

            if (!string.IsNullOrEmpty(ingredientTB.Text))
            {
                string ingredient = ingredientTB.Text.ToLower();
                filteredRecipes = filteredRecipes.Where(recipe =>
                    recipe.Ingredients.Any(ingredientName => ingredientName.ToLower().Contains(ingredient)));
            }

            if (foodGroupCB.SelectedIndex > 0)
            {
                var selectedItem = foodGroupCB.SelectedItem as ComboBoxItem;
                if (selectedItem != null)
                {
                    string selectedGroup = selectedItem.Content.ToString();
                    filteredRecipes = filteredRecipes.Where(recipe => recipe.FoodGroup == selectedGroup);
                }
            }

            if (!string.IsNullOrEmpty(caloriesTB.Text) && int.TryParse(caloriesTB.Text, out int maxCalories))
            {
                filteredRecipes = filteredRecipes.Where(recipe => recipe.Calories <= maxCalories);
            }

            recipeListView.ItemsSource = filteredRecipes.ToList();
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRecipes();
        }

        private void GeneratePieChart_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipes = recipeListView.SelectedItems.Cast<RecipeApplication>().ToList();
            if (selectedRecipes.Count == 0)
            {
                MessageBox.Show("Please select at least one recipe to generate the pie chart.");
                return;
            }

            var foodGroupCounts = selectedRecipes
                .GroupBy(r => r.FoodGroup)
                .Select(g => new { FoodGroup = g.Key, Count = g.Count() })
                .ToList();

            var pieSeries = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0
            };

            


        }

        private List<RecipeApplication> FetchRecipes()
        {
            return new List<RecipeApplication>
            {
                new RecipeApplication
                {
                    Ingredients = new List<string> { "Steak", "Salt", "Pepper" },
                    FoodGroup = "Protein",
                    Calories = 350
                },
                new RecipeApplication
                {
                    Ingredients = new List<string> { "Tomato", "Salt", "Basil" },
                    FoodGroup = "Vegetables",
                    Calories = 120
                }
            };
        }
    }

    public class RecipeApplication
    {
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
        public string FoodGroup { get; set; }
        public int Calories { get; set; }
    }
}