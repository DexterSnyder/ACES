using System;
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

                // cycle though the lines of output untill it runs out and get the last line 
                while (output != null)
                {
                    // if it is the first line of a git log pull all log data 
                    if (output.Contains("Author:"))
                    {
                        // get git author on first line 
                        string author = output;
                        output = cmd.StandardOutput.ReadLine();

                        // get git commit date on third line  
                        string date = output;
                        output = cmd.StandardOutput.ReadLine();
                        output = cmd.StandardOutput.ReadLine();

                        // get git commit massage starting on sixth line  
                        string massage = output.Trim();
                        output = cmd.StandardOutput.ReadLine();
                        output = cmd.StandardOutput.ReadLine();

                        // if only one commit massage line then this is the line changes.  
                        string linechanges = output;

                        // if there is a multiline massage. pull lines until it finds the correct line. 
                        while (!linechanges.Contains("file") && !linechanges.Contains("changed"))
                        {
                            output = cmd.StandardOutput.ReadLine();
                            output = cmd.StandardOutput.ReadLine();
                            linechanges = output;
                        }

                        //  create new commit object 
                        GitCommit commitData = new GitCommit();

                        // put parsed data into commit object 
                        commitData.PopulateDataFields(date, massage ,author, linechanges);

                        // add commit to commit list. 
                        current.commits.Add(commitData);
                    }
                    
                    // get next line of output for next loop 
                    output = cmd.StandardOutput.ReadLine();
                }

                // check for errors 
                string error = cmd.StandardError.ReadLine();

                // cycle though the lines of output until it runs out and get the last line 
                while (error != null)
                {
                    // if there is an error throw excption 
                    if (error == "remote: Invalid username or password.")
                    {
                        throw new Exception("Invalid username or password.");
                    }
                    else if (error.Contains("fatal: repository") && error.Contains("not found"))
                    {
                      
                        Console.Error.Write("repository not found for user " + current.Name);
                    }

                    // get the last line in output.
                    error = cmd.StandardError.ReadLine();
                }

                // get number of total commits for student. 
                current.NumStudentCommits = current.commits.Count;
              
            }// for loop

        }

        

    }//class
}//namespace
