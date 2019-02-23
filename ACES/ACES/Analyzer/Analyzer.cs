using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ACES
{

    class Analyzer
    {
        /// <summary>
        /// The current class being run
        /// </summary>
        public ClassRoom CurrentClass { get; private set; }

        /// <summary>
        /// System interface layer
        /// </summary>
        public SystemInterface CurrentSystem { get; private set; }

        private bool hasRun;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Analyzer ()
        {
            CurrentSystem = new SystemInterface();
            CurrentClass = new ClassRoom("", "");
            hasRun = false;
        }
       
        /// <summary>
        /// This is the main method the runs the analyzer
        /// </summary>
        /// <param name="nameOfOrgansation">Name of the GitHub Organization</param>
        /// <param name="rosterLocation">File location of the roster. Must be CSV</param>
        /// <param name="assignmentName">Name of the assigment</param>
        /// <param name="targetFolder">The folder to save student projects</param>
        /// <param name="userkey">User key for GitHub</param>
        /// <param name="unitTestLocation">The file location of the instructors unit test</param>
        /// <param name="gradingKey">The security code used for grading</param>
        public void run (string nameOfOrgansation, string rosterLocation, string assignmentName, 
            string targetFolder, string userkey, string unitTestLocation, string gradingKey)
        {
            hasRun = true;
            //get the class list, and load it up
            CurrentClass = new ClassRoom(nameOfOrgansation, rosterLocation);
            CurrentClass.CloneStudentRepositorys(assignmentName, targetFolder, userkey);

            string projectLocation = "";
            //pull projects from GitHub and build them
            foreach (Student student in CurrentClass.Students)
            {
                //set the project location
                projectLocation = targetFolder + "//" + student.Name;
                //build each class and get a score
                student.StudentScore = CurrentSystem.BuildAssignment(projectLocation, unitTestLocation, gradingKey);
                Analyze(student);
            }
        }

        /// <summary>
        /// Returns a list of the students
        /// </summary>
        /// <returns>List of students</returns>
        public ObservableCollection<Student> GetStudents()
        {
            if (!hasRun)
            {
                throw new Exception("Student list not populated");
            }

            return CurrentClass.Students;
        }

        /// <summary>
        /// Main method for running anti cheating algorithms
        /// </summary>
        /// <param name="student">The student to analyze</param>
        private void Analyze(Student student)
        {
            if (!hasRun)
            {
                throw new Exception("Student list not populated");
            }

            //get the commits

            //run analysis on commits
        }

    }//class 
}//namespace
