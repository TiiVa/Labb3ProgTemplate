using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Labb3ProgTemplate.Managerrs;
using Labb3ProgTemplate.Views;

namespace Labb3ProgTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() // 
        {
            InitializeComponent();
            UserManager.LoadUsersFromFile();
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged; 
        }

        private void UserManager_CurrentUserChanged() 
        {
            if (UserManager.IsAdminLoggedIn)
            {
                AdminTab.Visibility = Visibility.Visible;
                ShopTab.Visibility = Visibility.Visible;
                LoginTab.Visibility = Visibility.Collapsed;
                LoginViewO.Visibility = Visibility.Collapsed;
                AdminTab.IsSelected = true;

            }
            else if (UserManager.IsCustomerLoggedIn)
            {
                AdminTab.Visibility = Visibility.Collapsed;
                ShopTab.Visibility = Visibility.Visible;
                LoginTab.Visibility = Visibility.Collapsed;
                LoginViewO.Visibility = Visibility.Collapsed;
                ShopTab.IsSelected = true;
            }
            else if (UserManager.CurrentUser is null)
            {
                ShopTab.Visibility = Visibility.Collapsed;
                AdminTab.Visibility = Visibility.Collapsed;
                LoginTab.Visibility = Visibility.Visible;
                LoginViewO.Visibility = Visibility.Visible;
                AdminViewO.Visibility = Visibility.Collapsed;
                LoginTab.IsSelected = true;
            }
            else
            {

                ShopTab.Visibility = Visibility.Visible; 
                AdminTab.Visibility = Visibility.Collapsed; 
                LoginTab.Visibility = Visibility.Collapsed; 
                LoginViewO.Visibility = Visibility.Collapsed; 
                AdminViewO.Visibility = Visibility.Collapsed; 
                ShopTab.IsSelected = true;
            }
        }
    }
}
