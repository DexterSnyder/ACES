using ACES;
using ACES_GUI.CreateClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        ObservableCollection<Class> classList = new ObservableCollection<Class>();

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
            throw new NotImplementedException();
        }

        private void GetClassList()
        {
            throw new NotImplementedException();
        }

        private void CreateClass_Click(object sender, RoutedEventArgs e)
        {
            var createWindow = new CreateClass.CreateClass();
            createWindow.ShowDialog();
        }
    }
}
