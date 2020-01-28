#ifndef _AUTOMATON_H
#define _AUTOMATON_H

#include <string.h> 
#include<iostream>
using namespace std;
// Max number of states in the matching machine.
// Should be equal to the sum of the length of all keywords.
#define MAXS  20

// Maximum number of characters in input alphabet
#define MAXC  5
class Automaton {
public:

	// FAILURE FUNCTION IS IMPLEMENTED USING f[]
	int f[MAXS];

	// GOTO FUNCTION (OR TRIE) IS IMPLEMENTED USING g[][]
	int g[MAXS][MAXC];

	// OUTPUT FUNCTION IS IMPLEMENTED USING out[] 
	// Bit i in this mask is one if the word with index i 
	// appears when the machine enters this state. 
	int out[MAXS];
	Automaton(char** arr, int k);

	int buildAutomat(char** arr, int k);


	int findNextState(int currentState, char nextInput);
};

#endif