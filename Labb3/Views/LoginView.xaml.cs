using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net.Security;
using System.Windows;
using System.Windows.Controls;
using Labb3ProgTemplate.DataModels.Users;
using Labb3ProgTemplate.Enums;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {

        

        public LoginView()
        {
            InitializeComponent();
            
            UserManager.CurrentUserChanged += UserManager_CurrentUserChanged;
            

        }

        private void UserManager_CurrentUserChanged()
        {
            
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            UserManager.ChangeCurrentUser(LoginName.Text, LoginPwd.Password, UserTypes.Admin);
        }

        private void RegisterAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            UserManager.RegisterNewUser(RegisterName.Text, RegisterPwd.Password, UserTypes.Admin);
        }

        private void RegisterCustomerBtn_OnClickmerBtn_Click(object sender, RoutedEventArgs e)
        {
            UserManager.RegisterNewUser(RegisterName.Text, RegisterPwd.Password, UserTypes.Customer);
        }
    }
}
