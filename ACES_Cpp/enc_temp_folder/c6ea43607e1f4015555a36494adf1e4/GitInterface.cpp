//Visual Studio flags ctime as unsafe. Need to suppress the warnings
#define _CRT_SECURE_NO_WARNINGS 0

#include <stdlib.h>
#include <time.h> 
#include <string>


void gitCommit()
{
	//get the time in string form - Credit : http://www.cplusplus.com/reference/ctime/localtime/
	time_t rawtime;
	struct tm * timeinfo;

	time(&rawtime);
	timeinfo = localtime(&rawtime);
	std::string tempTime = asctime(timeinfo);

	//now we have to trim the /n off the end of the string
	tempTime = tempTime.substr(0, tempTime.size() - 1);

	//build the command to commit to the local branch
	std::string commitCommand = "git commit -m \"" +tempTime;
	commitCommand += "\"";

	//run commmands on the cmd line
	system("git add -A");
	system(commitCommand.c_str());

	//clean up
	delete timeinfo;
}


int main()
{
	gitCommit();
}