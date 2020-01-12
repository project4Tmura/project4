

#include "Search.h"
#include <iostream>



int main(){
  //Pair pairs[8]={(0,'a'),(0,'a'),(0,'a'),(0,'a'),(0,'a'),(0,'a'),(0,'a'),(0,'a')};
	// Pair p0(0,'a');
	// Pair p1(0,'b');
	// Pair p2(0,'c');
	// Pair p3(2,'a');
	// Pair p4(2,'b');
	// Pair p5(4,'c');
	// Pair p6(3,'a');
	// Pair p7(3,'b');

  	Pair p0(0,'a');
	Pair p1(1,'c');
	Pair p2(0,'g');
	Pair p3(2,'g');
	Pair p4(1,'t');


  Pair pairs[5]={p0,p1,p2,p3,p4};
  Automaton a(pairs, 5);
	
	//print the array
    for (int i = 0; i < MAXC; i++) {
    	for (int j = 0; j < MAXS; j++) {
    		 cout << a.g[j][i]<<"\t";
    	}
    	cout<<endl;
	}
	
	cout<<endl<<endl;
    for(int i=0;i<MAXS;i++){
        cout<<a.f[i]<<"\t";
    }
cout<<endl<<endl;

string text[] = {"a", "ac", "cb"}; 
int length = sizeof(text)/sizeof(text[0]);

Search s(&a);
s.searchWords(text, length); 

return 0;
}
