// C++ program for implementation of Aho Corasick algorithm
using namespace std;
#include "Automaton.h"
#include <cstring>
#include <iostream>
#include <queue> 



  void Automaton::buildAutomat(Pair pairs[],int length)
  {

      // Initialize all values in goto function as -1.
      memset(g, -1, sizeof g);

      int index,next;

    //fill goto array
      for(int i=0; i<length;i++)
      {
        index=pairs[i].getNode();
        next=pairs[i].getEdge()-'a';
        g[index][next]=i+1;
      }
      
      //For all characters which don't have an edge from
      // root (or state 0) in Trie, add a goto edge to state
      // 0 itself
      for (int ch = 0; ch < MAXC; ++ch)
          if (g[0][ch] == -1)
              g[0][ch] = 0;


    
    //fill failure 

      //Initialize values in fail function
      memset(f, -1, sizeof f);

      // Failure function is computed in breadth first order
      // using a queue
      queue<int> q;

      // Iterate over every possible input
      for (int ch = 0; ch < MAXC; ++ch)
      {
          // All nodes of depth 1 have failure function value
          // as 0. For example, in above diagram we move to 0
          // from states 1 and 3.
          if (g[0][ch] != 0)
          {
              f[g[0][ch]] = 0;
              q.push(g[0][ch]);
          }
      }

      // Now queue has states 1 and 3
      while (q.size())
      {
          // Remove the front state from queue
          int state = q.front();
          q.pop();

          // For the removed state, find failure function for
          // all those characters for which goto function is
          // not defined.
          for (int ch = 0; ch <= MAXC; ++ch)
          {
              // If goto function is defined for character 'ch'
              // and 'state'
              if (g[state][ch] != -1)
              {
                  // Find failure state of removed state
                  int failure = f[state];

                  // Find the deepest node labeled by proper
                  // suffix of string from root to current
                  // state.
                  while (g[failure][ch] == -1)
                      failure = f[failure];

                  failure = g[failure][ch];
                  f[g[state][ch]] = failure;

              

                  // Insert the next level node (of Trie) in Queue
                  q.push(g[state][ch]);
              }
          }
      }

    

}



    Automaton::Automaton(Pair pairs[], int length){
        buildAutomat(pairs, length);
    }

    // Returns the next state the machine will transition to using goto
    // and failure functions.
    // currentState - The current state of the machine. Must be between
    //                0 and the number of states - 1, inclusive.
    // nextInput - The next character that enters into the machine.
    int Automaton::findNextState(int currentState, char nextInput, bool end)
    {
        int answer = currentState;
        int ch = nextInput - 'a';

        // If goto is not defined, use failure function
        while (g[answer][ch] == -1)
            answer = f[answer];
        if(end && answer==0)
          return 0;

        return g[answer][ch];
        
    }



