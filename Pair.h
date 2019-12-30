
#ifndef Pair
class Pair{
    private:
        int node;
        char edge;

    public:
        
        Pair(int node=0, char edge='a');
        int getNode();
        char getEdge();
        void setNode(int node);
        void setEdge(char edge);
};
#endif