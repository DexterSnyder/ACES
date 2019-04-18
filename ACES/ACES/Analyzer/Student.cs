using System;
using System.Collections.Generic;
using System.Reflection;

namespace ACES
{
    /// <summary>
    /// Each object represents one student
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Students Username
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Private backer for Rating
        /// </summary>
        private string rating;

        /// <summary>
        /// Rating. Setting to green will overwrite any previous rating
        /// Setting to yellow or red will preserve the highest rating
        /// </summary>
        public string Rating
        {
            get
            {
                return rating;
            }
            set
            {
                switch (value)
                {
                    case "Green":
                        rating = "Green";
                        break;
                    case "Red":
                        rating = "Red";
                        break;
                    case "Yellow":
                        YellowMarks++;
                        if (value == "Red")
                        {
                            break;
                        }
                        else
                        {
                            if (YellowMarks >= 3)
                            {
                                rating = "Red";
                            }
                            else
                            {

                            }
                            rating = "Yellow";
                        }
                        break;

                    default:
                        throw new Exception("Not a supported color");
                }
            }
        }

        /// <summary>
        /// The number of times yellow has been assigned
        /// </summary>
        public int YellowMarks { get; private set; }

        /// <summary>
        /// github user name
        /// </summary>
        public string GitHubUserName { get; set; }

        /// <summary>
        /// Score on the unit tests
        /// </summary>
        public Score StudentScore { get; set; }

        /// <summary>
        /// location of the students repo 
        /// </summary>
        public string ProjectLocation { get; set; }

        /// <summary>
        /// This is the number of commits that a student has performed
        /// </summary>
        public int NumStudentCommits { get; set; }

        /// <summary>
        /// List of the commits by this student
        /// </summary>
        public List<GitCommit> Commits;

        /// <summary>
        /// Average time between commits in seconds
        /// </summary>
        public double AvgTimeBetweenCommits { get; set; }

        /// <summary>
        /// The standard deviation of times between commits in seconds
        /// </summary>
        public double StdDev { get; set; }
        
        /// <summary>
        /// The minimum of the time between commits
        /// </summary>
        public double Min { get; set; }

        /// <summary>
        /// The maximum of the time between commits
        /// </summary>
        public double Max { get; set; }

        /// <summary>
        /// The compiler the student used
        /// </summary>
        public string Compiler { get; set; }

        /// <summary>
        /// This list contains the reasons why a student recived a yellow mark
        /// </summary>
        private List<string> reasonsWhy;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="name">Student Name</param>
        /// <param name="userName">Student Username</param>
        public Student(string name, string userName)
        {
            Name = name;
            rating = "Green";
            GitHubUserName = userName;
            Commits = new List<GitCommit>();
            StudentScore = new Score();
            ProjectLocation = "";
            NumStudentCommits = 0;
            YellowMarks = 0;
            AvgTimeBetweenCommits = 0;
            StdDev = 0;
            reasonsWhy = new List<string>();
            Min = 0;
            Max = 0;
            Compiler = "";
        }

        /// <summary>
        /// Returns a list of why student got their ratings
        /// </summary>
        /// <returns>List of why students got their ratings</returns>
        public List<string> getReasonsWhy()
        {
            return reasonsWhy;
        }

        /// <summary>
        /// Adds a reason of why students got their rating
        /// Formate: [Color]: [Reason]
        /// </summary>
        /// <param name="reason">The reason for the rating</param>
        public void addReasonWhy(string reason)
        {
            try
            {
                reasonsWhy.Add(reason);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
