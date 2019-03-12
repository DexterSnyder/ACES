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
        /// Rating. Setting to green will overwrite any previous rating
        /// Setting to yellow or red will preserve the highest rating
        /// </summary>
        public string Rating
        {
            get
            {
                return Rating;
            }
            set
            {
                switch (value)
                {
                    case "Green":
                        Rating = "Green";
                        break;
                    case "Red":
                        Rating = "Red";
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
                                Rating = "Red";
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
        private int yellowMarks;

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


        public Student(string name, string userName)
        {
            Name = name;
            Rating = "Green";
            GitHubUserName = userName;
            commits = new List<GitCommit>();
            StudentScore = new Score();
            ProjectLocation = "";
            NumStudentCommits = 0;
            yellowMarks = 0;
        }

    }
}
