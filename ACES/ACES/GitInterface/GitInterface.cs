using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace ACES
{
    struct score
    {
        public int numberCorrect;
        public int numberIncorrect;
    }

    class GitInterface
    {
        /// <summary>
        /// builds a students c++ assignment
        /// </summary>
        /// <param name="studentProjLocation">Folder location of the students project</param>
        /// <param name="instructorUnitTests">Directory location of the instructors unit tests</param>
        public score BuildAssignment (string studentProjLocation, string instructorUnitTests, string securityCode)
        {
            score tempScore = new score();
            tempScore.numberCorrect = 0;
            tempScore.numberIncorrect = 0;

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.Start();

            //Set up the cmd prompt to run the VS tools
            string batDirectory = "cd \"C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Enterprise\\VC\\Auxiliary\\Build\"";
            cmd.StandardInput.WriteLine(batDirectory);
            string runBat = "vcvars32.bat";
            cmd.StandardInput.WriteLine(runBat);

            //move to the directory
            string directoryMove = "cd \"" + studentProjLocation + "\"";
            cmd.StandardInput.WriteLine(directoryMove);

            //Delete the students unit test
            string deleteCmd = "del /f UnitTests.cpp";
            cmd.StandardInput.WriteLine(deleteCmd);

            //Copy the instructors unit test
            string moveCmd = "copy \"" + instructorUnitTests + "\" \"" + studentProjLocation + "\"";
            cmd.StandardInput.WriteLine(moveCmd);

            //Build the project
            string buildCmd = "cl /EHsc UnitTests_InstructorVersion.cpp";
            cmd.StandardInput.WriteLine(buildCmd);

            //Run the project
            string runCmd = "UnitTests_InstructorVersion.exe";
            cmd.StandardInput.WriteLine(runCmd);

            cmd.StandardInput.WriteLine("exit");

            List<string> lines = new List<string>();

            // cycle though the lines of output untill it runs out and get the last line 
            string output = cmd.StandardOutput.ReadLine();
            while (output != null)
            {
                // get the last line in output. 
                lines.Add(output);
                output = cmd.StandardOutput.ReadLine();
            }

            // cycle though the lines of output untill it runs out and get the last line 
            string error = cmd.StandardError.ReadLine();
            while (error != null)
            {
                // get the last line in output. 
                lines.Add(error);
                //
                error = cmd.StandardError.ReadLine();
            }

            string correctAnswer = securityCode + " Passed";
            foreach (var line in lines)
            {
                string temp = line.Trim();
                
                if (temp == "Failed test")
                {
                    tempScore.numberIncorrect++;
                }

                if (temp == correctAnswer)
                {
                    tempScore.numberCorrect++;
                }
                
            }

            return tempScore;
        }//build assignment

    }//class
}//namespace
