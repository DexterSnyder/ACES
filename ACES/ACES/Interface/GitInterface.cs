using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ACES
{
    /// <summary>
    /// GitHub interface. Contains methods to interact with GitHub and GitHub classroom
    /// </summary>
    public class GitInterface
    {

        /// <summary>
        /// Clone the student repo's onto the local machine
        /// </summary>
        /// <param name="assignmentName">Name of the assignment</param>
        /// <param name="targetFolder">The high level target folder</param>
        /// <param name="userkey">Username:Password</param>
        /// <param name="students">A list of the students</param>
        /// <param name="nameOfOrganization">The name of the GitHub org</param>
        public void CloneStudentRepositorys(string assignmentName, string targetFolder, string userkey, ObservableCollection<Student> students, string nameOfOrganization)
        {
            try
            {
                foreach (Student current in students)
                {
                    // if repo not found dont get the data. 
                    bool repoFound = true;

                    // set student repo location
                    current.ProjectLocation = targetFolder + "\\" + current.Name;

                    // start command process to run git
                    Process cmd = new Process();
                    cmd.StartInfo.FileName = "cmd.exe";
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.RedirectStandardOutput = true;
                    cmd.StartInfo.RedirectStandardError = true;
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.Start();

                    if (userkey.Contains("#"))
                    {
                        userkey = userkey.Substring(0, userkey.IndexOf("#")) + "\\" + userkey.Substring(userkey.IndexOf("#"));
                    }

                    // create git clone command 
                    string gitClone = "git clone https://" + userkey + "@github.com/" + nameOfOrganization + "/"
                        + assignmentName + "-" + current.GitHubUserName + ".git " + targetFolder + "\\" + current.Name;

                    //execute git clone command 
                    cmd.StandardInput.WriteLine(gitClone);

                    //have command window go to repo folder for student 
                    cmd.StandardInput.WriteLine("cd " + targetFolder + "\\" + current.Name);

                    //run get log command with flag to show changes 
                    cmd.StandardInput.WriteLine("git log --shortstat");

                    //exit command line to have a set number of lines of output 
                    cmd.StandardInput.WriteLine("exit");

                    // read first line of output for while condition 
                    string output = cmd.StandardOutput.ReadLine();

                    List<String> outList = new List<String>();

                    while (output != null) // &&repoFound
                    {
                        outList.Add(output);
                        // get next line of output for next loop 
                        output = cmd.StandardOutput.ReadLine();

                    }

                    // check for errors 
                    string error = cmd.StandardError.ReadLine();
                    List<String> errorList = new List<String>();
                    // cycle though the lines of error until it runs out and get the last line 
                    while (error != null)
                    {
                        errorList.Add(error);
                        error = cmd.StandardError.ReadLine();
                    }

                    // cycle though the lines of output until it runs out and get the last line 
                    foreach (string currentError in errorList)
                    {

                        // if there is an error throw excption 
                        if (currentError == "remote: Invalid username or password.")
                        {
                            throw new Exception("Invalid username or password.");
                        }
                        else if (currentError.Contains("fatal: repository") && currentError.Contains("not found"))
                        {
                            repoFound = false;
                            Console.Error.Write("repository not found for user " + current.Name);
                        }
                    }


                    if (repoFound)
                    {
                        // cycle though the lines of output untill it runs out and get the last line 
                        for (int c = 0; c < outList.Count; c++) // &&repoFound
                        {
                            // if it is the first line of a git log pull all log data 
                            if (outList[c].Contains("Author:"))
                            {

                                // get git author on first line 
                                string author = outList[c];
                                c++;

                                // get git commit date on third line  
                                string date = outList[c];
                                c = c + 2;

                                // get git commit massage starting on sixth line  
                                string massage = outList[c].Trim();
                                c = c + 2;

                                // if only one commit massage line then this is the line changes.  
                                string linechanges = outList[c];

                                if (linechanges.Contains("commit "))
                                {
                                    linechanges = "";
                                }
                                else
                                {
                                    // if there is a multiline massage. pull lines until it finds the correct line. 
                                    while (!linechanges.Contains("file") && !linechanges.Contains("changed"))
                                    {
                                        c = c + 2;
                                        linechanges = outList[c];
                                        if (linechanges.Contains("commit "))
                                        {
                                            linechanges = "";
                                            break;
                                        }
                                    }
                                }

                                //  create new commit object 
                                GitCommit commitData = new GitCommit();

                                // put parsed data into commit object 
                                commitData.PopulateDataFields(date, massage, author, linechanges);

                                // add commit to commit list. 
                                current.Commits.Add(commitData);
                            }
                        }
                    }

                }// for loop
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "."
                    + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }



    }//class
}//namespace
