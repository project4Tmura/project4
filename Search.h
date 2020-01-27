#ifndef _SEARCH_H
#define _SEARCH_H

#include "Automaton.h"
#include <string.h> 
#include<iostream>
using namespace std;
class Search {
private:
	Automaton* a;
public:
	Search(Automaton* a);
	int searchWords(char** arr, int k, char* text);
};

#endif
