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
            SaveClassList();
            //dropDown name: .ItemsSource  = classList; 
        }

        private void SaveClassList()
        {
            throw new NotImplementedException();
        }

        private void GetClassList()
        {
            throw new NotImplementedException();
        }
    }
}
