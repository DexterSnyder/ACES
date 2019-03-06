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
        /// Rating
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// github user name
        /// </summary>
        public string GitHubUserName;

        /// <summary>
        /// Score on the unit tests
        /// </summary>
        public Score StudentScore;

        /// <summary>
        /// location of the students repo 
        /// </summary>
        public string ProjectLocation;

        public List<GitCommit> commits;


        public Student(string name, string userName)
        {
            Name = name;
            Rating = "Green";
            GitHubUserName = userName;
            commits = new List<GitCommit>();
            StudentScore = new Score();
        }
    }
}
