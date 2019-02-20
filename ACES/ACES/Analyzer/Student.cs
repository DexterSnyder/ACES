using System;

namespace ACES
{
    /// <summary>
    /// class to old the students after being pulled from the site.
    /// </summary>
    class Student
    {
        /// <summary>
        /// Students Username
        /// </summary>
        String Name;
        
        /// <summary>
        /// Number of commits
        /// </summary>
        int Commits;
        
        /// <summary>
        /// Rating
        /// </summary>
        string Rating;
        
        /// <summary>
        /// github user name
        /// </summary>
        string GitHubUserName;
        
        /// <summary>
        /// Score on the unit tests
        /// </summary>
        int Score { get; set; }

        public string ProjectLocation;

        public Student(string name, string userName)
        {
            Name = name;
            Commits = 0;
            Rating = "Green";
            GitHubUserName = userName;
            Score = 0;
        }

        internal string GetUserName()
        {
            return GitHubUserName;
        }

        internal string GetName()
        {
            return Name;
        }

        internal void setCommits(int num)
        {
            this.Commits = num;
        }
    }
}
