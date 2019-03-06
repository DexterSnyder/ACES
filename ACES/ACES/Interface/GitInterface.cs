using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace ACES
{
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
            foreach (Student current in students)
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.Start();
 
                string gitClone = "git clone https://" + userkey + "@github.com/" + nameOfOrganization + "/" 
                    + assignmentName + "-" + current.GitHubUserName + ".git " + targetFolder + "\\" + current.Name;

                cmd.StandardInput.WriteLine(gitClone);

                cmd.StandardInput.WriteLine("cd " + targetFolder + "\\" + current.Name);

                cmd.StandardInput.WriteLine("git log --shortstat");

                cmd.StandardInput.WriteLine("exit");

                string output = cmd.StandardOutput.ReadLine();

                // cycle though the lines of output untill it runs out and get the last line 
                while (output != null)
                {
                    if (output.Contains("Author:"))
                    {
                        string author = output;
                        output = cmd.StandardOutput.ReadLine();
                        string date = output;
                        output = cmd.StandardOutput.ReadLine();
                        output = cmd.StandardOutput.ReadLine();
                        string massage = output.Trim();
                        output = cmd.StandardOutput.ReadLine();
                        output = cmd.StandardOutput.ReadLine();
                        string linechanges = output;
                        // if there is a multiline massage. pull lines until it finds the correct line. 
                        while (!linechanges.Contains("file") && !linechanges.Contains("changed"))
                        {
                            output = cmd.StandardOutput.ReadLine();
                            output = cmd.StandardOutput.ReadLine();
                            linechanges = output;
                        }

                        GitCommit commitData = new GitCommit();

                        commitData.PopulateDataFields(date, massage ,author, linechanges);

                        current.commits.Add(commitData);
                    }
                    
                    output = cmd.StandardOutput.ReadLine();
                }

                string error = cmd.StandardError.ReadLine();

                // cycle though the lines of output until it runs out and get the last line 
                while (error != null)
                {
                    if (error == "remote: Invalid username or password.")
                    {
                        throw new Exception("Invalid username or password.");
                    }
                    else if (error.Contains("fatal: repository") && error.Contains("not found"))
                    {
                      
                        Console.Error.Write("repository not found for user " + current.Name);
                    }
                    else
                    {
                        current.ProjectLocation = targetFolder + "\\" + current.Name;
                    }
                    // get the last line in output.
                    error = cmd.StandardError.ReadLine();
                }
              
            }// for loop

        }

        

    }//class
}//namespace
