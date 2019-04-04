using System;
using System.Collections.Generic;

namespace ACES
{
    /// <summary>
    /// class to old the students after being pulled from the site.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Students Username
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        ///Private backer for Rating
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
                        yellowMarks++;
                        if (value == "Red")
                        {
                            break;
                        }
                        else
                        {
                            if (yellowMarks >= 3)
                            {
                                rating = "Red";
                            }
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
        public int yellowMarks { get; private set; }

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
        public List<GitCommit> commits;

        /// <summary>
        /// Average time between commits in seconds
        /// </summary>
        public double avgTimeBetweenCommits { get; set; }

        /// <summary>
        /// The standard deviation of times between commits in seconds
        /// </summary>
        public double stdDev { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

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
            commits = new List<GitCommit>();
            StudentScore = new Score();
            ProjectLocation = "";
            NumStudentCommits = 0;
            yellowMarks = 0;
            avgTimeBetweenCommits = 0;
            stdDev = 0;
            reasonsWhy = new List<string>();
            min = 0;
            max = 0;
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
            reasonsWhy.Add(reason);
        }
    }
}
