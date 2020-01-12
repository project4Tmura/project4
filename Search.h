#ifndef Search
#include "Automaton.h"
#include <bits/stdc++.h>
using namespace std; 
class Search{
    private:
      Automaton * a;
    public:
        Search(Automaton * a);
        void searchWords(string text[], int length); 
};
#endif