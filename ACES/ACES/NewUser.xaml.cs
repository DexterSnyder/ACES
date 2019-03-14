using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ACES
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : Window
    {
        public NewUser()
        {
            InitializeComponent();
        }

        /// <summary>
        //On click for create user button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateUser(object sender, RoutedEventArgs e)
        {
            //reset colors
            UserBox.Background = Brushes.White;

            //Check that all fields are filled out
            if (UserBox.Text == null || UserBox.Text == "")
            {
                UserBox.Background = Brushes.Red;
                return;
            }
            if (PasswordBox.SecurePassword == null)
            {
                PasswordBox.Background = Brushes.Red;
                return;
            }
            if (PasswordConfirmBox.SecurePassword == null)
            {
                PasswordConfirmBox.Background = Brushes.Red;
                return;
            }

            //Save User data

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
