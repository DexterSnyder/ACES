//Visual Studio flags ctime as unsafe. Need to suppress the warnings
#define _CRT_SECURE_NO_WARNINGS 0

#include <ctime> 
#include "StacksAndQueues.h"

using std::cout;
using std::cerr;
using std::endl;
using std::string;

//This helps with testing, do not modify.
bool checkTest(string testName, int whatItShouldBe, int whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << "Passed " << testName << endl;
		return true;
	}
	else {
		cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been " << whatItShouldBe << endl;
		return false;
	}
}


//This helps with testing, comment it in when ready, but do not modify the code.
bool checkTest(string testName, string whatItShouldBe, string whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << "Passed " << testName << endl;
		return true;
	}
	else {
		if (whatItShouldBe == "") {
			cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been blank. " << endl;
		}
		else {
			cout << "***Failed test " << testName << " *** " << endl << "   Output was " << whatItIs << endl << "   Output should have been " << whatItShouldBe << endl;
		}
		return false;
	}
}

//This helps with testing, do not modify.
bool checkTestMemory(string testName, int whatItShouldBe, int whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << "Passed " << testName << endl;
		return true;
	}
	else {
		cout << "***Failed test " << testName << " *** " << endl << ".  ";
		cout << "You are manually managing " << whatItIs << " bytes in memory, but it should be " << whatItShouldBe << " bytes." << endl;
		return false;
	}
}



//This helps with testing, do not modify.
void testCourseStack() {

	string result;
	string caughtError;
	CourseStack<int> *stack = new CourseStack<int>(5);
	stack->push(1);

	int data = stack->pop();
	checkTest("testCourseStack #1", 1, data);

	stack->push(1);
	stack->push(2);
	stack->push(3);
	stack->push(4);
	stack->push(5);
	checkTest("testCourseStack #2", 5, stack->pop());
	checkTest("testCourseStack #3", 4, stack->pop());
	checkTest("testCourseStack #4", 3, stack->pop());
	checkTest("testCourseStack #5", 2, stack->pop());
	checkTest("testCourseStack #6", 1, stack->pop());

	//now cover error handling
	try {
		result = stack->pop();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest("testCourseStack #7", "caught", caughtError);

	//check currentSize
	checkTest("testCourseStack #8", 0, stack->size());
	stack->push(12);
	stack->push(32);
	checkTest("testCourseStack #9", 2, stack->size());

	//now test filling it up
	stack->push(14);
	stack->push(53);
	stack->push(47);
	checkTest("testCourseStack #10", 5, stack->size());

	//This should simply not let the 8 go in, as it is out of room.
	stack->push(8);

	//Grab all the items again.
	checkTest("testCourseStack #11", 47, stack->pop());
	checkTest("testCourseStack #12", 53, stack->pop());
	checkTest("testCourseStack #13", 14, stack->pop());
	checkTest("testCourseStack #14", 32, stack->pop());
	checkTest("testCourseStack #15", 12, stack->pop());

	//now do error handling again
	try {
		result = stack->pop();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest("testCourseStack #16", "caught", caughtError);

	delete stack;

	//test some strings
	CourseStack<string> *sstack = new CourseStack<string>(10);

	sstack->push("pencil");
	sstack->push("pen");
	sstack->push("marker");

	checkTest("testCourseStack #17", 3, sstack->size());

	//remove pen from the stack.
	string temp = sstack->pop();
	sstack->pop();
	sstack->push(temp);

	//see if it worked 
	checkTest("testCourseStack #18", "marker", sstack->pop());
	checkTest("testCourseStack #19", "pencil", sstack->pop());

	checkTest("testCourseStack #20", 0, sstack->size());
	delete sstack;

}

//This helps with testing, comment it in when ready, but do not modify the code.
void testCourseQueue() {

	string result;
	string caughtError;
	CourseQueue<string> *pQueue = new CourseQueue<string>(5);

	//Tests push_back
	pQueue->push_back("penny");
	pQueue->push_back("nickel");
	pQueue->push_back("dime");
	pQueue->push_back("quarter");

	checkTest("testCourseQueue #1", 4, pQueue->size());

	checkTest("testCourseQueue #2", "penny", pQueue->pop_front());
	checkTest("testCourseQueue #3", 3, pQueue->size());

	checkTest("testCourseQueue #4", "nickel", pQueue->pop_front());
	checkTest("testCourseQueue #5", "dime", pQueue->pop_front());
	checkTest("testCourseQueue #6", "quarter", pQueue->pop_front());
	checkTest("testCourseQueue #7", 0, pQueue->size());

	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest("testCourseQueue #8", "caught", caughtError);
	checkTest("testCourseQueue #9", 0, pQueue->size());

	//Try it again.  This should make it wrap around, and fill up.
	pQueue->push_back("penny");
	pQueue->push_back("nickel");
	pQueue->push_back("dime");
	pQueue->push_back("quarter");

	checkTest("testCourseQueue #10", "penny", pQueue->pop_front());
	pQueue->push_back("half dollar");
	pQueue->push_back("silver dollar");

	//It should be full, no more room to add more.
	pQueue->push_back("million dollar bill");

	checkTest("testCourseQueue #12", "nickel", pQueue->pop_front());
	checkTest("testCourseQueue #13", "dime", pQueue->pop_front());
	checkTest("testCourseQueue #14", "quarter", pQueue->pop_front());
	checkTest("testCourseQueue #15", "half dollar", pQueue->pop_front());
	checkTest("testCourseQueue #16", "silver dollar", pQueue->pop_front());
	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest("testCourseQueue #17", "caught", caughtError);

	//Test adding and removing back and forth
	pQueue->push_back("penny");
	checkTest("testCourseQueue #17", "penny", pQueue->pop_front());
	pQueue->push_back("nickel");
	checkTest("testCourseQueue #18", "nickel", pQueue->pop_front());
	pQueue->push_back("dime");
	checkTest("testCourseQueue #19", "dime", pQueue->pop_front());
	pQueue->push_back("quarter");
	checkTest("testCourseQueue #20", "quarter", pQueue->pop_front());
	pQueue->push_back("half dollar");
	checkTest("testCourseQueue #21", "half dollar", pQueue->pop_front());
	pQueue->push_back("silver dollar");
	checkTest("testCourseQueue #22", 1, pQueue->size());

	checkTest("testCourseQueue #23", "silver dollar", pQueue->pop_front());
	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest("testCourseQueue #24", "caught", caughtError);

	delete pQueue;

}

void pressAnyKeyToContinue() {
	cout << "Press any key to continue...";

	//Linux and Mac users with g++ don't need this
	//But everyone else will see this message.
#ifndef __GNUC__
	_getch();

#else
	int c;
	fflush(stdout);
	do c = getchar(); while ((c != '\n') && (c != EOF));
#endif
	cout << endl;
}

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

	std::string compiler = " - ";

	//define compiler
	/*CLion uses other compilers in their tool chain
	see: https://www.jetbrains.com/help/clion/how-to-switch-compilers-in-clion.html */

#ifdef _MSC_VER //Visual Studio
	compiler += "Microsoft_Visual_Studio";
#elif __GNUC__ //gcc
	compiler += "GCC";
#elif __clang__ //clang
	compiler += "Clang";
#elif __MINGW32__ //minGW 32 bit
	compiler += "MinGW_32_bit";
#elif __MINGW64__ //minGW 64 bit
	compiler += "MinGW_64_bit";
#elif __BORLANDC__ //Borland
	compiler += "Borland";
#elif __TINYC__ //Tiny C
	compiler += "Tiny_C";
#else
	compiler += "Unknown";
#endif
	//More listed here: https://sourceforge.net/p/predef/wiki/Compilers/

	//build the command to commit to the local branch
	std::string commitCommand = "git commit -m \"" + tempTime + compiler;
	commitCommand += "\"";

	//Add something in the future to set this
	std::string userName = "Default";
	std::string userNameCmd = "git config user.name \"" + userName + "\"";
	std::string email = "Default@Default.com";
	std::string emailCmd = "git config user.email \"" + email + "\"";

	//run commmands on the cmd line
	system("git add -A");

	//these don't accually have to run everytime, but this is the most straitforward way
	system(userNameCmd.c_str());
	system(email.c_str());

	system(commitCommand.c_str());

	//clean up - throws an exception when timeinfo is deleted
	//delete timeinfo;
}

int main() {

	{
		gitCommit();
		testCourseStack();
		pressAnyKeyToContinue();
		testCourseQueue();
		pressAnyKeyToContinue();
		pressAnyKeyToContinue();
	}
	cout << "Shutting down the program" << endl;
	return 0;
}
