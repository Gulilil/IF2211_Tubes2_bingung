using System

namespace src

class Point

int row
int col

// ctor
Point ()

    row <- 0 col <- 0 


Point(int r, int c)

    row <- r col <- c 


Point(Point p)

    this.row <- p.getRow()
    this.col <- p.getCol()


// setter getter
int getRow()

    -> row


int getCol()

    -> col


procedure setRow(int r)

    this.row <- r


procedure setCol(int c)

    this.col <- c


// other methods
procedure copyPoint(Point p)
    this.row <- p.getRow()
    this.col <- p.getCol()

procedure goLeft()

    this.col--


procedure goRight()

    this.col++


procedure goUp() 

    this.row--


procedure goDown()

    this.row++


// override object.Equals
override bool Equals(object obj)
            
    if (obj = null or GetType() !<- obj.GetType())
    
        -> false
    
    Point p <- (Point) obj
    -> (getRow() = p.getRow() and getCol() = p.getCol())


// override object.GetHashCode
override int GetHashCode()

    -> (row,col).GetHashCode()


bool isLeftOf(Point p)

    -> (this.getRow() = p.getRow() and this.getCol() = p.getCol()-1)

bool isRightOf(Point p)

    -> (this.getRow() = p.getRow() and this.getCol() = p.getCol()+1)


bool isUpOf(Point p)

    -> (this.getRow() = p.getRow()-1 and this.getCol() = p.getCol())

bool isDownOf(Point p)

    -> (this.getRow() = p.getRow()+1 and this.getCol() = p.getCol())


// print and display
procedure displayPoint()
    output("("+this.row +", "+this.col+")")



