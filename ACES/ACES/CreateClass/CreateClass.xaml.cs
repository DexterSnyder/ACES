using ACES;
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
using System.Windows.Shapes;

namespace ACES_GUI.CreateClass
{
    /// <summary>
    /// Interaction logic for CreateClass.xaml
    /// </summary>
    public partial class CreateClass : Window
    {
        ObservableCollection<Class> classList;

        public CreateClass(ObservableCollection<Class> cl)
        {
            InitializeComponent();

            classList = cl;
        }

        private void createClassButton_Click(object sender, RoutedEventArgs e)
        {
            Class testClass = new Class(classroomName.Text, rosterFile.Text);

            classList.Add(testClass);
            this.Close();
        }

    }
}
