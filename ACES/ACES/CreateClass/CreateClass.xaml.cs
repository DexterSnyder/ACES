using ACES;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace ACES_GUI.CreateClass
{
    /// <summary>
    /// Interaction logic for CreateClass.xaml
    /// </summary>
    public partial class CreateClass : Window
    {
        ObservableCollection<ClassRoom> classList;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cl"></param>
        public CreateClass(ObservableCollection<ClassRoom> cl)
        {
            InitializeComponent();

            classList = cl;
        }

        /// <summary>
        /// On click for the create class button. Creates a new class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createClassButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClassRoom testClass = new ClassRoom(orgName.Text, rosterFileBox.Text, classroomName.Text);

                classList.Add(testClass);
                this.Close();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Allows the user to browes for a roster file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseForRoster(object sender, RoutedEventArgs e)
        {
            try
            {
                FileDialog dialog = new OpenFileDialog();

                if (dialog.ShowDialog() == true)
                {
                    string fullPath = dialog.FileName;
                    rosterFileBox.Text = fullPath;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                            MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Handle the error.
        /// </summary>
        /// <param name="sClass">The class in which the error occurred in.</param>
        /// <param name="sMethod">The method in which the error occurred in.</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                //Would write to a file or database here.
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (Exception ex)
            {
                System.IO.File.AppendAllText("C:\\Error.txt", Environment.NewLine +
                                             "HandleError Exception: " + ex.Message);
            }
        }
    }
}
