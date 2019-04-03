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
        public void run (ClassRoom CurrentClass, string assignmentName, string targetFolder, 
                              string userkey, string unitTestLocation, string gradingKey)
        {
            hasRun = true;
            //get the class list, and load it up
            CurrentClass.CloneStudentRepositorys(assignmentName, targetFolder, userkey);

            foreach (Student student in CurrentClass.Students)
            {
                //build each class and get a score
                student.StudentScore = CurrentSystem.BuildAssignment(student.ProjectLocation, unitTestLocation, gradingKey);
                analyze(student);
            }

            //Calculate the std dev for the class
            List<int> averages = new List<int>();
            int classAvg = 0;
            //loop through and get data for each student
            foreach (Student student in CurrentClass.Students)
            {  
                classAvg += (int) student.avgTimeBetweenCommits;
                averages.Add((int)student.avgTimeBetweenCommits);
            }

            //calculate the class average time between commits, and the standerd dev
            //of the averages
            classAvg = classAvg / CurrentClass.Students.Count();
            CurrentClass.AvgStdDev = (int)Math.Sqrt(averages.Sum(x => Math.Pow(x - classAvg, 2))
                / (CurrentClass.Students.Count() - 1));

            int lowerThreshold = classAvg - (2 * CurrentClass.AvgStdDev);

            //now, find the students with commits below the lower threshold
            foreach (Student student in CurrentClass.Students)
            {
                if ( (int)student.avgTimeBetweenCommits < lowerThreshold)
                {
                    student.Rating = "Yellow";
                    student.addReasonWhy("Yellow: Avg time between commits below threshold");
                }
            }

        }//run

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
        private void analyze(Student student)
        {
            if (!hasRun)
            {
                throw new Exception("Student list not populated");
            }

            List<double> commitTimes = new List<double>();

            //run analysis on commits//////////////////

            bool authorFlag = false;
            bool firstFlag = true;
            //first, check how many commits they have done
            foreach (GitCommit commit in student.commits)
            {
                //check for instructor commits
                if (commit.CommitMessageDateTime.Equals(new DateTime()))
                {
                    continue;
                }

                //Make sure that they have run the program, and 
                //that the author doesn't switch mid work
                bool tempAuthorFlag = false;
                if (commit.Author == "Default")
                {
                    authorFlag = true;
                    tempAuthorFlag = true;
                    student.NumStudentCommits++;
                }

                /*
                * authorFlag shows that Default was found once, while
                * tempAuthor flag shows that it was found each time
                * If author flag is set, but temp isn't there was a change of
                * author mid assignment. Red Flag
                */
                if (authorFlag && !tempAuthorFlag)
                {
                    student.Rating = "Red";
                    student.addReasonWhy("Red: Change in author mid assignment");
                }

                //Get the average time between commits
                //first, handle if it is the first commit
                if (firstFlag)
                {
                    commitTimes.Add(0);
                    firstFlag = false;
                }
                else
                {
                    //Don't need a null check, no way to reach this code if commits[0] is null
                    //The first commit serves as a "0" time frame
                    TimeSpan t = (student.commits[0].CommitDateTime - commit.CommitMessageDateTime);
                    double timeHours = (double)t.TotalHours;
                    
                    //now store it
                    commitTimes.Add(timeHours);
                }                

            }//foreach

            //These values can be adjusted for sensitivity
            //analyze number of commits
            if (student.NumStudentCommits < 2)
            {
                student.Rating = "Red";
                student.addReasonWhy("Red: number of commits below threshold");
            }
            else if (student.NumStudentCommits < 5)
            {
                student.Rating = "Yellow";
                student.addReasonWhy("Yellow: number of commits below threshold");
            }

            //get the average between commits
            student.avgTimeBetweenCommits = CalcAvgTime(commitTimes);

            double max = 0;
            double min = 0;

            //now analyze
            if (commitTimes.Count() > 0)
            {
                max = commitTimes.Max();
                min = commitTimes.Min();
            }
            
            student.stdDev = (int) Math.Sqrt(commitTimes.Sum(x => Math.Pow(x - student.avgTimeBetweenCommits, 2)) 
                / (commitTimes.Count - 1));
            
        } //void analyze(Student student)

        /// <summary>
        /// Calculates the avg time between the times in the list
        /// </summary>
        /// <param name="times">A list of the times to average</param>
        /// <returns>An average in a ulong</returns>
        private double CalcAvgTime(List<double> times)
        {
            if (times.Count == 0)
            {
                return 0;
            }

            double avg = 0;

            foreach (double time in times)
            {
                avg += time;
            }

            avg = avg / (double)times.Count;

            return avg;
        }

        
    }//class 

}//namespace
