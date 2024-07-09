using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;


namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {

        public ObservableCollection<Product> ProductsList { get; set; }

        public ObservableCollection<RealProduct> CustomerCartList { get; set; }

        public RealProduct CustomerSelectedItem { get; set; }

        public RealProduct ShopSelectedItem { get; set; }


        public ShopView()
        {
            InitializeComponent();
            ProductManager.ProductListChanged += ProductManagerOnProductListChanged;
            ProductsList = new ObservableCollection<Product>();
            CustomerCartList = new ObservableCollection<RealProduct>();
            UserManager.CurrentUserChanged +=
                UserManager_CurrentUserChanged;

            DataContext = this;

            ProductsList.Add(new Vegetable("Tomato", 5, ProductTypes.Vegetable) { Name = "Tomato", Price = 5.0 });
            ProductsList.Add(new Vegetable("Cucumber", 4, ProductTypes.Vegetable) { Name = "Cucumber", Price = 4.0 });
            ProductsList.Add(new Vegetable("Lettuce", 7, ProductTypes.Vegetable) { Name = "Lettuce", Price = 7 });
            ProductsList.Add(new Fruit("Apple", 3, ProductTypes.Fruit) { Name = "Apple", Price = 3 });
            ProductsList.Add(new Fruit("Banana", 8, ProductTypes.Fruit) { Name = "Banana", Price = 8 });
            ProductsList.Add(new Fruit("Orange", 5.5, ProductTypes.Fruit) { Name = "Orange", Price = 5.5 });

        }

        private void ProductManagerOnProductListChanged()
        {
            ProductsList.Clear();

            foreach (var product in ProductManager.Products)
            {
                ProductsList.Add(product);
            }
        }

        private void UserManager_CurrentUserChanged()
        {
            if (UserManager.CurrentUser is null)
            {
                return;
            }
            foreach (var realProduct in UserManager.CurrentUser.Cart)
            {
                CustomerCartList.Add(realProduct);
            }
        }

        private void RemoveBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.RemoveFromCart(CustomerSelectedItem);
            CustomerCartList.Remove(CustomerSelectedItem);

        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.AddToCart(ShopSelectedItem);
            CustomerCartList.Add(ShopSelectedItem);

        }

        private void LogoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.LogOut();

        }

        private void CheckoutBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            UserManager.CurrentUser.Cart.Clear();
            CustomerCartList.Clear();
        }
    }
}
