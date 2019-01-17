#define _CRT_SECURE_NO_WARNINGS 0

#include <stdlib.h>
#include <time.h> 
#include <string>


void gitCommit()
{
	//get the time in string form
	time_t rawtime;
	struct tm * timeinfo;

	time(&rawtime);
	timeinfo = localtime(&rawtime);
	std::string tempTime = asctime(timeinfo);

	//now we have to trim the /n off the end of the string
	tempTime = tempTime.substr(0, tempTime.size() - 1);

	std::string commitCommand = "git commit -m \"" +tempTime;
	commitCommand += "\"";
	//run commmands on the cmd line
	system("git add -A");
	//system("git commit -m \"" + my_time + "\"");
	system(commitCommand.c_str());
}


int main()
{
	gitCommit();
}