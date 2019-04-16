using System;
using ACES;

using System.Windows;
using ACES_GUI;

namespace ACES.UserLogin
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Tests to make sure that the GitHub user name and password are correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void TestPassword(object sender, RoutedEventArgs e)
        {
            
            try
            {
                UserInfo test = new UserInfo(UserBox.Text, PasswordBox.Password);
                await test.TestLogin();
                ((MainWindow)this.Owner).SetUser(UserBox.Text, PasswordBox.Password);
                MessageBox.Show("Login Sucessful", "Massage", MessageBoxButton.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
