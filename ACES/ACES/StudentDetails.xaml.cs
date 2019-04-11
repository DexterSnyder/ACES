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
    /// Interaction logic for StudentDetails.xaml
    /// </summary>
    public partial class StudentDetails : Window
    {
        private Student student;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="student">The student to display</param>
        public StudentDetails(Student student)
        {
            InitializeComponent();

            //make sure we have a student
            if (student == null)
            {
                this.Hide();
                return;
            }

            this.student = student;
            DisplayStudent(student);
            
        }//constructor

        /// <summary>
        /// Sets the GUI to display the student
        /// </summary>
        /// <param name="student">The student to display</param>
        public void DisplayStudent(Student student)
        {
            //Set the ListBox
            List<string> temp = student.getReasonsWhy();
            ReasonsWhy.ItemsSource = temp;

            //Calculate the time. Integer division is done intentionally
            int seconds = (int)student.AvgTimeBetweenCommits;
            int days = seconds / 86400;
            seconds = seconds % 86400;
            int hours = seconds / 3600;
            seconds = seconds % 3600;
            int minutes = seconds / 60;
            seconds = seconds % 60;

            //set the GUI controls
            AvgTimeBetweenCommitsValue.Content = days + ":" + hours
                + ":" + minutes + ":" + seconds;

            StudentNameLabel.Content = student.Name;
            TotalNumCommitsValue.Content = student.NumStudentCommits;
            StdDevCommitsValue.Content = student.StdDev;
            RatingValue.Content = student.Rating;
            YellowMarksValue.Content = student.YellowMarks;
            AvgTimeBetweenCommitsValue.Content = student.AvgTimeBetweenCommits;
            ScoreValue.Content = student.StudentScore.NumberCorrect + " / " + 
                (student.StudentScore.NumberCorrect + student.StudentScore.NumberIncorrect);
        }

        /// <summary>
        /// Called on close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
