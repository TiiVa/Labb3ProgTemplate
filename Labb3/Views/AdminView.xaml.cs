using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for AdminView.xaml
    /// </summary>
    public partial class AdminView : UserControl
    {
        public ObservableCollection<Product> ProductsList { get; set; }

        public string EditProductName { get; set; } = string.Empty;

        public string EditProductPrice { get; set; } = string.Empty;
        public string EditProductGroup { get; set; } = string.Empty;

        public Product SelectedItem { get; set; }

        public AdminView()
        {
            InitializeComponent();

            
            ProductManager.LoadProductsFromFile();

            ProductsList = new();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            ProductManager.ProductListChanged += ProductManager_ProductListChanged;

            DataContext = this;

            ProductsList.Add(new Vegetable("Tomato", 5, ProductTypes.Vegetable) { Name = "Tomato", Price = 5.0 });
            ProductsList.Add(new Vegetable("Cucumber", 4, ProductTypes.Vegetable) { Name = "Cucumber", Price = 4.0 });
            ProductsList.Add(new Vegetable("Lettuce", 7, ProductTypes.Vegetable) { Name = "Lettuce", Price = 7 });
            ProductsList.Add(new Fruit("Apple", 3, ProductTypes.Fruit) { Name = "Apple", Price = 3 });
            ProductsList.Add(new Fruit("Banana", 8, ProductTypes.Fruit) { Name = "Banana", Price = 8 });
            ProductsList.Add(new Fruit("Orange", 5.5, ProductTypes.Fruit) { Name = "Orange", Price = 5.5 });
        }


        private void ProductManager_ProductListChanged()
        {
            ProductsList.Clear();

            foreach (var product in ProductManager.Products)
            {
                ProductsList.Add(product);
            }

            
        }

        private void UserManager_CurrentUserChanged()
        {
            
        }

        private void ProdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            if (ProdList.SelectedItem is Product selectedItem)
            {
                NameBox.Text = selectedItem.Name;
                PriceBox.Text = selectedItem.Price.ToString();
                ProductGroupBox.Text = selectedItem.Type.ToString();
            }
        }

        private void SaveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (SelectedItem is not null)
            {
                SelectedItem.Name = NameBox.Text;
                SelectedItem.Price = double.Parse(PriceBox.Text);

            } 
            else
            {
                if (ProductGroupBox.Text == "Vegetable")
                {
                    ProductManager.AddProduct(new Vegetable(NameBox.Text, double.Parse(PriceBox.Text), ProductTypes.Vegetable));
                }
                else if (ProductGroupBox.Text == "Fruit")
                {
                    ProductManager.AddProduct(new Fruit(NameBox.Text, double.Parse(PriceBox.Text), ProductTypes.Fruit));
                }
            }
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            if (SelectedItem is null)
            {
                return;
            }

            ProductManager.RemoveProduct(SelectedItem); 
        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();


        }
    }
}
