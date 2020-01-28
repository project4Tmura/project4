//----------------------------------------------------------------------------------------
// This file define the extern functions we will call from the c# project
//----------------------------------------------------------------------------------------

#include "Automaton.h"    
#include "Search.h"

#ifdef __cplusplus
extern "C" {
#endif

	extern __declspec(dllexport) Automaton* creat_Automaton(char** array, int arraySize);

	extern __declspec(dllexport) Search* creat_search(Automaton* automaton);

	extern __declspec(dllexport) int do_searchWords(Search* search, char** array, int arraySize, char* text);

	

#ifdef __cplusplus
}
#endif
