#include "Extern.h"

Automaton* creat_Automaton(char** array, int arraySize) {
	return new Automaton(array, arraySize);
}

Search* creat_search(Automaton* automaton) {
	return new Search(automaton);
}

int do_searchWords(Search* search, char** array, int arraySize, char* text) {
	return search->searchWords(array, arraySize, text);
}

int addnums(int a, int b) {
	return a + b;
}
