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

namespace ACES
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<Class> classList = new ObservableCollection<Class>();

        public MainWindow()
        {
            InitializeComponent();
            GetClassList();

            classbox.ItemsSource  = classList; 
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
                    foreach (Class current in classList)
                    {
                        sw.WriteLine(current.getOrgName() + "," + current.getRosterLocation());
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
                    foreach (Class current in classList)
                    {
                        sw.WriteLine(current.getOrgName() + "," + current.getRosterLocation());
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
                        classList.Add(new Class(items[0], items[1]));
                    }

                }
            }
        }

        private void test(object sender, RoutedEventArgs e)
        {
            classList.Add(new Class("DexterSnyderTestOrg", "C:\\Users\\Ethgar\\Documents\\School\\acesTesting\\classroom_roster.csv"));

            classList.First().CloneStudentRepositorys("assignment1", "C:\\Users\\Ethgar\\Documents\\School\\acesTesting", "Adamvans:8my8w5PdYt92");
        }

        private void saveInfo(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveClassList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GitInterface temp = new GitInterface();
            string studentFolder = "C:\\Users\\dexte\\OneDrive\\Desktop\\SE3\\Student1";
            string instructorKey = "C:\\Users\\dexte\\OneDrive\\Desktop\\SE3\\UnitTests_InstructorVersion.cpp";
            //temp.BuildAssignment(studentFolder, instructorKey, "23456");
        }
    }
}
