using System

namespace src

class Tile 

char value
int visitedCount

// ctor
Tile() 
    value <- '.'
    visitedCount <- 0


// setter getter
procedure setValue(char c) 
    value <- c

procedure setVisitedCount(int vc)
    visitedCount <- vc

char getValue()
    -> value

int getVisitedCount()
    -> visitedCount


// other method
procedure increaseVisitedCount()
    visitedCount++



