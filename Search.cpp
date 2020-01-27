#include "Search.h"

Search::Search(Automaton* a) {
	this->a = a;
}

// This function finds all occurrences of all array words 
// in text. 
int Search::searchWords(char** arr, int k, char* text)
{
	
	int* exist_errors = new int[k];
	memset(exist_errors, 0, sizeof exist_errors);
	// Initialize current state 
	int currentState = 0;
	const string& textref = text;

	// Traverse the text through the nuilt machine to find 
	// all occurrences of words in arr[] 

	for (int i = 0; i < textref.size(); ++i)
	{

		currentState = a->findNextState(currentState, text[i]);
		// If match not found, move to next state 
		if (a->out[currentState] == 0)
			continue;

		// Match found, print all matching words of arr[] 
		// using output function. 
		for (int j = 0; j < k; ++j)
		{

			if (a->out[currentState] & (1 << j))
			{
				//the error arr[j] exist in the text
				exist_errors[j] = 1;


			//	const string& word = arr[j];
			//	cout << "The known error " << arr[j] << " appears from "
			//		<< i - word.size() + 1 << " to " << i << endl;
			}

		}

	}

	//removing the not existing errors from the array
	for (int i = 0; i < k; i++)
	{
		if (exist_errors[i] == 0) {
			const string& word = arr[i];
			strcpy_s( arr[i], word.size(), "-1");
		}
			
	}

	return 0;
}