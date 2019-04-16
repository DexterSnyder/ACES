using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ACES
{

    /// <summary>
    /// Represents a GitHub classroom object
    /// </summary>
    public class ClassRoom
    {
        /// <summary>
        /// List containing the student objects
        /// </summary>
        public ObservableCollection<Student> Students { get; private set; }

        /// <summary>
        /// Name of the GitHub organizations
        /// </summary>
        public string NameOfOrganization { get; set; }

        /// <summary>
        /// Name of the class in the tool. 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The file location of the roster
        /// </summary>
        public string RosterLocation { get; set; }

        /// <summary>
        /// Github interface layer
        /// </summary>
        private GitInterface git;

        /// <summary>
        /// The average std dev for the class
        /// </summary>
        public int AvgStdDev { get; set; }

       
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="nameOfOrgansation">Name of the GitHub organization</param>
        /// <param name="rosterLocation">File location of the roster</param>
        public ClassRoom(string nameOfOrgansation, string rosterLocation, string name)
        {
            NameOfOrganization = nameOfOrgansation;
            RosterLocation = rosterLocation;
            Name = name;
            Students = new ObservableCollection<Student>();
            GetStudentsFromRoster();
            git = new GitInterface();
        }

        /// <summary>
        /// Default Constructor.
        /// </summary>
        private ClassRoom()
        { 
            //Don't use
        }

        /// <summary>
        /// Loads the students from the roster
        /// </summary>
        public void GetStudentsFromRoster()
        {
            try
            {
                StreamReader sr = new StreamReader(RosterLocation);
                // read the fiirst linw which is just a key. 
                string input = sr.ReadLine();
                // read the first data line. 
                input = sr.ReadLine();

                while (input != null)
                {
                    // split the line into a list of items 
                    string[] line = input.Split(',');

                    // only input students that have connected to github classroom 
                    if (line[1].Trim('"') != "")
                    {
                        // get the student username and id set by the teacher. 
                        Students.Add(new Student(line[0].Trim('"'), line[1].Trim('"')));
                    }
                    //Read the next line
                    input = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Clone the student repositories
        /// </summary>
        /// <param name="assignmentName"></param>
        /// <param name="targetFolder"></param>
        /// <param name="userkey"></param>
        public void CloneStudentRepositorys (string assignmentName, string targetFolder, string userkey)
        {
            try
            {
                //broke this out to preserve proper layer architeture 
                git.CloneStudentRepositorys(assignmentName, targetFolder, userkey, Students, NameOfOrganization);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// metod to show the list of classes in the dropdown. need id for class. 
        /// </summary>
        override
        public string ToString()
        {
            return Name;
        }

    }//class
}//namespace
