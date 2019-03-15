using ACES;
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
        ObservableCollection<ClassRoom> classList = new ObservableCollection<ClassRoom>();

        UserInfo currentUser;

        Analyzer Analyze; 

        public MainWindow()
        {
            InitializeComponent();
            GetClassList();
            RosterDataGrid();
            Analyze = new Analyzer(); 
            classComboBox.ItemsSource = classList;
        }

        //Datagrid for displaying students assignment scores, info, etc.
        private void RosterDataGrid()
        {
            /*
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            DataGridTextColumn col4 = new DataGridTextColumn();
            DataGridTextColumn col5 = new DataGridTextColumn();
            DataGridTextColumn col6 = new DataGridTextColumn();
           // studentFilesList.Columns.Add(col1);
            studentFilesList.Columns.Add(col2);
            studentFilesList.Columns.Add(col3);
            studentFilesList.Columns.Add(col4);
            studentFilesList.Columns.Add(col5);
            studentFilesList.Columns.Add(col6);
            //col1.Binding = new Binding("Name");
            col2.Binding = new Binding("Tests Passed");
            col3.Binding = new Binding("Commits");
            col4.Binding = new Binding("Avg Commit Time");
            col5.Binding = new Binding("Std Dev");
            col6.Binding = new Binding("Compiler Used");
           // col1.Width = 83.33;
            col2.Width = 83.33;
            col3.Width = 83.33;
            col4.Width = 83.33;
            col4.Width = 83.33;
            col4.Width = 83.33;
           // col1.Header = "Name";
            col2.Header = "Tests Passed";
            col3.Header = "Commits";
            col4.Header = "Avg Commit Time";
            col5.Header = "Std Dev";
            col6.Header = "Compiler Used";
            */
        }

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
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation);
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
                        sw.WriteLine(current.NameOfOrganization + "," + current.RosterLocation);
                    }
                }
            }
        }

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
                        classList.Add(new ClassRoom(items[0], items[1]));
                    }

                }
            }
        }

        private void CreateClass_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateClass.CreateClass(classList);
            createWindow.ShowDialog();
        }

        private void RunChecks(object sender, RoutedEventArgs e)
        {
            string useKey = currentUser.userName + ":" + currentUser.password;

            Analyze.run((ClassRoom)classComboBox.SelectedItem, assignTextBox.Text, RepoFolderBox.Text,
                            useKey, UnitTestLocationBox.Text, "gradeing key");
        }


        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            studentFilesList.ItemsSource = ((ClassRoom)classComboBox.SelectedItem).Students;
        }

        private void CreateLoginWindow(object sender, RoutedEventArgs e)
        {
            LoginWindow createWindow = new LoginWindow();
            createWindow.Owner = this;
            createWindow.ShowDialog();
        }

        internal void SetUser(string userName, string password)
        {
            currentUser = new UserInfo(userName, password);
            createClassButton.IsEnabled = true;
            checkFilesButton.IsEnabled = true;
        }

        private void BrowseForUnitTest(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            { 
                UnitTestLocationBox.Text = dialog.FileName;
            }
        }

        private void BrowseForRepoFolder(object sender, RoutedEventArgs e)
        {
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = Environment.CurrentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = Environment.CurrentDirectory;
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
    }
}