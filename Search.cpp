// C++ program for implementation of Aho Corasick algorithm 
// for string matching 
#include "Search.h"

  
  

  Search::Search(Automaton* a){
    this->a = a;
  }


  // This function finds all occurrences of all array words 
  // in text. 
  void Search::searchWords(string text[], int length) 
  {
    for(int l = 0; l < length; ++l)
    {
      // Initialize current state 
      int currentState = 0; 
      bool end=false;
      // Traverse the text through the nuilt machine to find 
      // all occurrences of words in arr[] 
      for (int i = 0; i < text[l].size(); ++i) 
      { 
        if(i==text[l].size()-1)
          end=true;
          currentState = a->findNextState(currentState, text[l][i],end); 
    
          // // If match not found, move to next state 
          // if (out[currentState] == 0) 
          //     continue; 
    
          // // Match found, print all matching words of arr[] 
          // // using output function. 
          // for (int j = 0; j < k; ++j) 
          // { 
          //     if (out[currentState] & (1 << j)) 
          //     { 
          //         cout <<"in " << text[l] << " Word " << arr[j] << " appears from "
          //             << i - arr[j].size() + 1 << " to " << i << endl; 
          //     } 
          // } 
          cout << currentState << endl;
      }
      if(currentState != 0){
        cout << endl << "the word " << text[l] << " appears" << endl;
      
      } 
    }
  }




  
 
  
 
  
 
  
// // Driver program to test above 
// int main() 
// { 
//     string arr[] = {"he", "she", "hers", "his"}; 
//     string text[] = {"ahishers", "sheh", "thfge"}; 
    
//     int k = sizeof(arr)/sizeof(arr[0]);
//     int length = sizeof(text)/sizeof(text[0]);

//     // Preprocess patterns. 
//     // Build machine with goto, failure and output functions 
//     buildMatchingMachine(arr, k); 
//     Search s;
//     s.searchWords(arr, k, text, length); 
  
//     return 0; 
// } 