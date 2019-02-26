using System;

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
        /// Number of commits
        /// </summary>
        public int Commits { get; set; }

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
       

        public Student(string name, string userName)
        {
            Name = name;
            Commits = 0;
            Rating = "Green";
            GitHubUserName = userName;
            StudentScore = new Score();
        }

        internal void setCommits(int num)
        {
            this.Commits = num;
        }
    }
}
