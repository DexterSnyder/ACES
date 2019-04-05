﻿using ACES;
using ACES.UserLogin;
using ACES_GUI.CreateClass;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ACES_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// A list of the saved classes
        /// </summary>
        ObservableCollection<ClassRoom> classList = new ObservableCollection<ClassRoom>();

        /// <summary>
        /// Stores the users GitHub user name and password
        /// </summary>
        UserInfo currentUser;

        /// <summary>
        /// The object that contains the methods to do the buisness logic
        /// </summary>
        Analyzer Analyze; 

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            GetClassList();
            Analyze = new Analyzer(); 
            classComboBox.ItemsSource = classList;

            createClassButton.Visibility = Visibility.Hidden;
            checkFilesButton.Visibility = Visibility.Hidden;
            deleteClassBtn.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Saves the .csv files that contains the saved classes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveClassList(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // create a default path that is only used in the program. 
            string path = "classlist.csv";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    // write all data to the file. 
                    foreach (ClassRoom current in classList)
                    {
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation + "," + current.Name);
                    }

                }
            }
            else
            {
                // empty file if it exists 
                File.WriteAllText(path, "");
                using (StreamWriter sw = new StreamWriter(path))
                {
                    // write all data to the file. 
                    foreach (ClassRoom current in classList)
                    {
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation + "," + current.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Saves a .csv file that contains the saved classes
        /// </summary>
        private void SaveClassList()
        {
            // create a default path that is only used in the program. 
            string path = "classlist.csv";

            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    // write all data to the file. 
                    foreach (ClassRoom current in classList)
                    {
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation + "," + current.Name);
                    }

                }
            }
            else
            {
                // empty file if it exists 
                File.WriteAllText(path, "");
                using (StreamWriter sw = new StreamWriter(path))
                {
                    // write all data to the file. 
                    foreach (ClassRoom current in classList)
                    {
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation + "," + current.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the classes saved by the user in the .csv file
        /// </summary>
        private void GetClassList()
        {
            // create a default path that is only used in the program. 
            string path = "classlist.csv";

            if (File.Exists(path))
            {
                // Create a file to write to.
                using (StreamReader sr = File.OpenText(path))
                {
                    string currentLine = "";
                    while ((currentLine = sr.ReadLine()) != null && currentLine != "")
                    {
                        string[] items = currentLine.Split(',');
                        classList.Add(new ClassRoom(items[0], items[1], items[2]));
                    }

                }
            }
        }

        /// <summary>
        /// On click button that opens the window to create a class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateClass_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateClass.CreateClass(classList);
            createWindow.ShowDialog();
            SaveClassList();
            classComboBox.SelectedIndex = classComboBox.Items.Count - 1;
        }

        /// <summary>
        /// Runs the checks to analyze the students
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunChecks(object sender, RoutedEventArgs e)
        {
            assignTextBox.Background = Brushes.White;
            UnitTestLocationBox.Background = Brushes.White;
            RepoFolderBox.Background = Brushes.White;
            SecurityKeyBox.Background = Brushes.White;

            //Data validation
            if (classComboBox.SelectedValue == null ||
                classComboBox.SelectedValue.ToString() == "")
            {
                MessageBoxResult result = MessageBox.Show("Please select or create a class");
                return;
            }
            if (assignTextBox.Text == null || assignTextBox.Text == "")
            {
                assignTextBox.Background = Brushes.Red;
                return;
            }
            if (UnitTestLocationBox.Text == null || UnitTestLocationBox.Text == "")
            {
                UnitTestLocationBox.Background = Brushes.Red;
                return;
            }
            if (RepoFolderBox.Text == null || RepoFolderBox.Text == "")
            {
                RepoFolderBox.Background = Brushes.Red;
                return;
            }
            if (SecurityKeyBox.Text == null || SecurityKeyBox.Text == "")
            {
                SecurityKeyBox.Background = Brushes.Red;
                return;
            }
            


            string useKey = currentUser.userName + ":" + currentUser.password;

            Analyze.run((ClassRoom)classComboBox.SelectedItem, assignTextBox.Text, RepoFolderBox.Text,
                            useKey, UnitTestLocationBox.Text, SecurityKeyBox.Text);

            studentFilesList.Items.Refresh();
        }

        /// <summary>
        /// updates the students that are displayed when the selected class is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((ClassRoom)classComboBox.SelectedItem != null)
            {
                studentFilesList.ItemsSource = ((ClassRoom)classComboBox.SelectedItem).Students;
            }
            else
            {
                studentFilesList.ItemsSource = null;
            }
            
        }

        /// <summary>
        /// Opens a window for the user to log into GitHub
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginWindow createWindow = new LoginWindow();
            createWindow.Owner = this;
            createWindow.ShowDialog();
        }

        /// <summary>
        /// Opens the window that allows the user to log into GitHub
        /// Displays the buttons once the user logs in
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        internal void SetUser(string userName, string password)
        {
            currentUser = new UserInfo(userName, password);
            createClassButton.Visibility = Visibility.Visible;
            checkFilesButton.Visibility = Visibility.Visible;
            deleteClassBtn.Visibility = Visibility.Visible;


        }

        /// <summary>
        /// Opens the browser to allow the user to select the unit test file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseForUnitTest(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            { 
                UnitTestLocationBox.Text = dialog.FileName;
            }
        }

        /// <summary>
        /// Opens hte browser to allow the user to select the location to clone student repos too
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseForRepoFolder(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                RepoFolderBox.Text = dlg.FileName;
            }

        }

        /// <summary>
        /// On click that deletes a class
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DelClass_Click(object sender, RoutedEventArgs e)
        {
            classList.Remove((ClassRoom)classComboBox.SelectedItem);
            classComboBox.ItemsSource = classList;
        }

        /// <summary>
        /// Displays window based on selected student
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Row_DoubleClick (object sender, RoutedEventArgs e)
        {
            Student temp = (Student) studentFilesList.SelectedItem;
            StudentDetails details = new StudentDetails(temp);
            details.ShowDialog();
        }
    }
}