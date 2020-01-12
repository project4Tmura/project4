#ifndef Automaton
#include "Pair.h"
// Max number of states in the matching machine.
// Should be equal to the sum of the length of all keywords.
#define MAXS  20

// Maximum number of characters in input alphabet
#define MAXC  5
class Automaton{
    public:

      // FAILURE FUNCTION IS IMPLEMENTED USING f[]
      int f[MAXS];

      // GOTO FUNCTION (OR TRIE) IS IMPLEMENTED USING g[][]
      int g[MAXS][MAXC];

      void buildAutomat(Pair pairs[],int length);

    public:
        Automaton(Pair pairs[], int length);
        int findNextState(int currentState, char nextInput,bool end);
};
#endif