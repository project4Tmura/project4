//----------------------------------------------------------------------------------------
// This file implements the extern functions we will call from the c# project
//----------------------------------------------------------------------------------------
#include "Extern.h"

Automaton* creat_Automaton(char** array, int arraySize) {
	return new Automaton(array, arraySize);
}

Search* creat_search(Automaton* automaton) {
	return new Search(automaton);
}

int do_searchWords(Search* search, char** array, int arraySize, char* text) {

	//searching for existing errors
	int ret= search->searchWords(array, arraySize, text); 

	//release memory
	search->delete_outomat();
	delete search;

	return ret;
}
