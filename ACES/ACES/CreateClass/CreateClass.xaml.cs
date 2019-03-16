﻿using ACES;
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

        public CreateClass(ObservableCollection<ClassRoom> cl)
        {
            InitializeComponent();

            classList = cl;
        }

        private void createClassButton_Click(object sender, RoutedEventArgs e)
        {
            ClassRoom testClass = new ClassRoom(orgName.Text, rosterFileBox.Text, classroomName.Text);

            classList.Add(testClass);
            this.Close();
        }

        private void BrowseForRoster(object sender, RoutedEventArgs e)
        {
            FileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                string fullPath = dialog.FileName;
                rosterFileBox.Text = fullPath; 
            }
        }
    }
}
