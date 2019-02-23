using ACES;
using ACES_GUI.CreateClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

        public MainWindow()
        {
            InitializeComponent();
            GetClassList();
            SaveClassList();
            RosterDataGrid();
        }

        private void RosterDataGrid()
        {
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();
            DataGridTextColumn col3 = new DataGridTextColumn();
            DataGridTextColumn col4 = new DataGridTextColumn();
            studentFilesList.Columns.Add(col1);
            studentFilesList.Columns.Add(col2);
            studentFilesList.Columns.Add(col3);
            studentFilesList.Columns.Add(col4);
            col1.Binding = new Binding("Name");
            col2.Binding = new Binding("Score");
            col3.Binding = new Binding("Rating");
            col4.Binding = new Binding("Result");
            col1.Width = 78.5;
            col2.Width = 78.5;
            col3.Width = 78.5;
            col4.Width = 78.5;
            col1.Header = "Name";
            col2.Header = "Score";
            col3.Header = "Rating";
            col4.Header = "Result";
        }

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
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] items = currentLine.Split(',');
                        classList.Add(new ClassRoom(items[0], items[1]));
                    }

                }
            }
        }

        private void CreateClass_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateClass.CreateClass();
            createWindow.ShowDialog();
        }

        private async void RunChecks(object sender, RoutedEventArgs e)
        {
            // teseting login 
            //try
            //{
            //    UserInfo test = new UserInfo("Adamvans", "8my8w5PdYt92");
            //    await test.login();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            //}
            
            // testing commit logging 
            // classList[0].CloneStudentRepositorys("Assignment1", "C:\\Users\\Ethgar\\Documents\\School\\acesTesting", "Adamvans:8my8w5PdYt92");
        }
    }
}
