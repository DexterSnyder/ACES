//#include "Catch2.h"
#include "StacksAndQueues.h"

using std::cout;
using std::cerr;
using std::endl;
using std::string;

std::string securityCode = "23456";

//This helps with testing, do not modify.
bool checkTest_I(string testName, int whatItShouldBe, int whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << securityCode << " Passed" << endl;
		return true;
	}
	else {
		cout << "Failed test " << endl;
		return false;
	}
}


//This helps with testing, comment it in when ready, but do not modify the code.
bool checkTest_I(string testName, string whatItShouldBe, string whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << securityCode << " Passed" <<  endl;
		return true;
	}
	else {
		cout << "Failed test " << endl;
		return false;
	}
}

//This helps with testing, do not modify.
bool checkTestMemory_I(string testName, int whatItShouldBe, int whatItIs) {

	if (whatItShouldBe == whatItIs) {
		cout << securityCode << " Passed" <<  endl;
		return true;
	}
	else {
		cout << "Failed test " << endl;
		return false;
	}
}

//This helps with testing, do not modify.
void testCourseStack_I() {

	string result;
	string caughtError;
	CourseStack<int> *stack = new CourseStack<int>(5);
	stack->push(1);

	int data = stack->pop();
	checkTest_I("testCourseStack #1", 1, data);

	stack->push(1);
	stack->push(2);
	stack->push(3);
	stack->push(4);
	stack->push(5);
	checkTest_I("testCourseStack #2", 5, stack->pop());
	checkTest_I("testCourseStack #3", 4, stack->pop());
	checkTest_I("testCourseStack #4", 3, stack->pop());
	checkTest_I("testCourseStack #5", 2, stack->pop());
	checkTest_I("testCourseStack #6", 1, stack->pop());

	//now cover error handling
	try {
		result = stack->pop();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest_I("testCourseStack #7", "caught", caughtError);

	//check currentSize
	checkTest_I("testCourseStack #8", 0, stack->size());
	stack->push(12);
	stack->push(32);
	checkTest_I("testCourseStack #9", 2, stack->size());

	//now test filling it up
	stack->push(14);
	stack->push(53);
	stack->push(47);
	checkTest_I("testCourseStack #10", 5, stack->size());

	//This should simply not let the 8 go in, as it is out of room.
	stack->push(8);

	//Grab all the items again.
	checkTest_I("testCourseStack #11", 47, stack->pop());
	checkTest_I("testCourseStack #12", 53, stack->pop());
	checkTest_I("testCourseStack #13", 14, stack->pop());
	checkTest_I("testCourseStack #14", 32, stack->pop());
	checkTest_I("testCourseStack #15", 12, stack->pop());

	//now do error handling again
	try {
		result = stack->pop();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest_I("testCourseStack #16", "caught", caughtError);

	delete stack;

	//test some strings
	CourseStack<string> *sstack = new CourseStack<string>(10);

	sstack->push("pencil");
	sstack->push("pen");
	sstack->push("marker");

	checkTest_I("testCourseStack #17", 3, sstack->size());

	//remove pen from the stack.
	string temp = sstack->pop();
	sstack->pop();
	sstack->push(temp);

	//see if it worked 
	checkTest_I("testCourseStack #18", "marker", sstack->pop());
	checkTest_I("testCourseStack #19", "pencil", sstack->pop());

	checkTest_I("testCourseStack #20", 0, sstack->size());
	delete sstack;

}

//This helps with testing, comment it in when ready, but do not modify the code.
void testCourseQueue_I() {

	string result;
	string caughtError;
	CourseQueue<string> *pQueue = new CourseQueue<string>(5);

	//Tests push_back
	pQueue->push_back("penny");
	pQueue->push_back("nickel");
	pQueue->push_back("dime");
	pQueue->push_back("quarter");

	checkTest_I("testCourseQueue #1", 4, pQueue->size());

	checkTest_I("testCourseQueue #2", "penny", pQueue->pop_front());
	checkTest_I("testCourseQueue #3", 3, pQueue->size());

	checkTest_I("testCourseQueue #4", "nickel", pQueue->pop_front());
	checkTest_I("testCourseQueue #5", "dime", pQueue->pop_front());
	checkTest_I("testCourseQueue #6", "quarter", pQueue->pop_front());
	checkTest_I("testCourseQueue #7", 0, pQueue->size());

	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest_I("testCourseQueue #8", "caught", caughtError);
	checkTest_I("testCourseQueue #9", 0, pQueue->size());

	//Try it again.  This should make it wrap around, and fill up.
	pQueue->push_back("penny");
	pQueue->push_back("nickel");
	pQueue->push_back("dime");
	pQueue->push_back("quarter");

	checkTest_I("testCourseQueue #10", "penny", pQueue->pop_front());
	pQueue->push_back("half dollar");
	pQueue->push_back("silver dollar");

	//It should be full, no more room to add more.
	pQueue->push_back("million dollar bill");

	checkTest_I("testCourseQueue #12", "nickel", pQueue->pop_front());
	checkTest_I("testCourseQueue #13", "dime", pQueue->pop_front());
	checkTest_I("testCourseQueue #14", "quarter", pQueue->pop_front());
	checkTest_I("testCourseQueue #15", "half dollar", pQueue->pop_front());
	checkTest_I("testCourseQueue #16", "silver dollar", pQueue->pop_front());
	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest_I("testCourseQueue #17", "caught", caughtError);

	//Test adding and removing back and forth
	pQueue->push_back("penny");
	checkTest_I("testCourseQueue #17", "penny", pQueue->pop_front());
	pQueue->push_back("nickel");
	checkTest_I("testCourseQueue #18", "nickel", pQueue->pop_front());
	pQueue->push_back("dime");
	checkTest_I("testCourseQueue #19", "dime", pQueue->pop_front());
	pQueue->push_back("quarter");
	checkTest_I("testCourseQueue #20", "quarter", pQueue->pop_front());
	pQueue->push_back("half dollar");
	checkTest_I("testCourseQueue #21", "half dollar", pQueue->pop_front());
	pQueue->push_back("silver dollar");
	checkTest_I("testCourseQueue #22", 1, pQueue->size());

	checkTest_I("testCourseQueue #23", "silver dollar", pQueue->pop_front());
	caughtError = "not caught";
	try {
		result = pQueue->pop_front();
	}
	catch (int e) {
		caughtError = "caught";
	}
	checkTest_I("testCourseQueue #24", "caught", caughtError);

	delete pQueue;

}

void pressAnyKeyToContinue_I() {
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


int main() {

	{
		testCourseStack_I();
		//pressAnyKeyToContinue_I();
		testCourseQueue_I();
		//pressAnyKeyToContinue_I();
	}
	cout << "Shutting down the program" << endl;
	return 0;
}
