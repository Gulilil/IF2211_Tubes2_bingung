using System

namespace src

class Map

 int row
 int col
 int nTreasure
 Point[] treasureLocs
 Point startLoc
 Point curLoc
 Tile[,] buffer
 bool valid

// ctor
Map() 
    this.row <- 0
    this.col <- 0
    this.nTreasure <- 0
    this.treasureLocs <- new Point[] 
    this.startLoc <- new Point()
    this.curLoc <- new Point()
    this.buffer <- new Tile[0,0] 
    this.valid <- true


// setter getter
void setRow(int r)
    row <- r


void setCol(int c)
    col <- c


int getRow()
    -> row
        

int getCol()
    -> col


void setCurLoc(Point p)

    this.curLoc <- p


Point getCurLoc()
    -> this.curLoc

Point getStartLoc()

    -> this.startLoc

int getnTreasure() 
    -> this.nTreasure

char getValueAtCoordinate(int r, int c)
    -> this.buffer[r,c].getValue()

char getValueAtCoordinate(Point p)
    -> this.buffer[p.getRow(), p.getCol()].getValue()

void setValueAtCoordinate(Point p, char c)
    this.buffer[p.getRow(), p.getCol()].setValue(c)

void setVCAtCoordinate(Point p, int n)
    this.buffer[p.getRow(), p.getCol()].setVisitedCount(n)

int getVCAtCoordinate(int r, int c)

    -> this.buffer[r, c].getVisitedCount()

int getVCAtCoordinate(Point p)
    -> this.buffer[p.getRow(), p.getCol()].getVisitedCount()

void increaseVCAtCoordinate(Point p)
    this.buffer[p.getRow(), p.getCol()].increaseVisitedCount()

Point[] getTreasureLocations()
    -> this.treasureLocs

void setValidTrue()

    this.valid <- true

void setValidFalse()

    this.valid <- false

bool getValid()

    -> this.valid


// other methods
void changeCurLoc(char c)
    if (c <-<- 'L')
        this.curLoc.goLeft()
     else if (c <-<- 'R')
        this.curLoc.goRight()
     else if (c <-<- 'U')
        this.curLoc.goUp()
     else if (c <-<- 'D')
        this.curLoc.goDown()
    


void addTreasureLocation(int r, int c)
    Point treasure <- new Point(r,c)
    Point[] temp <- (Point[])this.treasureLocs.Clone()
    this.treasureLocs <- new Point[temp.Length+1]
    for (int i <- 0 i < this.treasureLocs.Length i++)
        if (i <-<- this.treasureLocs.Length -1)
            this.treasureLocs[i] <- treasure
         else 
            this.treasureLocs[i] <- temp[i]
        
    


void getInfo()
    if (getValid())
    
        System.output("<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-<-")
        displayMap()
        output("Row: " + getRow())
        output("Col: " + getCol())
        output("Treasure Amount: " + getnTreasure())
        output("Treasure Locations: ")
        displayTreasureLocations()
        output("Starting Location: ")
        this.startLoc.displayPoint()
        System.output()
    


void resetMap()

    if (getValid())
    
        this.curLoc.copyPoint(this.startLoc)

        for (int i <- 0 i < row i++)
        
            for (int j <- 0 j < col j++)
            
                setVCAtCoordinate(new Point(i, j), 0)
            
        
    


// print and display
void displayMap()
    for(int i <- 0 i < this.row i++)
        output("[ ")
        for (int j <- 0 j < this.col j++)
            if (j <-<- this.col-1)
                output(this.buffer[i,j].getValue())
             else 
                output(this.buffer[i,j].getValue())
                output(' ')
            
        
        output(" ]")
        output()
    




// read file
void IdentifyFile(string textFile)
    int nCol <- 0
    int nRow <- 0
    string[] lines <- File.ReadAllLines(textFile)
    foreach (string line in lines)
        if (nCol <-<- 0)
            foreach(char c in line)
                if (c !<- ' ')
                    nCol++
                    if (c !<- 'K' && c !<- 'X' && c!<- 'R' && c !<- 'T')
                    
                        setValidFalse()
                    
                
            
        
        nRow++
    
    this.setCol(nCol)
    this.setRow(nRow)


void ReadFile()

    output("Enter File Name: ")
    string? fileName <- Console.ReadLine()
    fileName <- fileName + ".txt"
    string path <- Directory.GetCurrentDirectory()
    string fullPath <- path + "/test/" +fileName

    IdentifyFile(fullPath)
    if (getValid())
    
        this.buffer <- new Tile[this.row, this.col]
        for (int i <- 0 i < row i++)
        
            for (int j <- 0 j < col j++)
            
                this.buffer[i, j] <- new Tile()
            
        
        string[] lines <- File.ReadAllLines(fullPath)

        int nCol <- 0
        int nRow <- 0
        foreach (string line in lines)
        
            nCol <- 0
            foreach (char c in line)
            
                if (c !<- ' ')
                
                    this.buffer[nRow, nCol].setValue(c)

                    if (c <-<- 'T')
                    
                        this.nTreasure++
                        addTreasureLocation(nRow, nCol)
                    
                    else if (c <-<- 'K')
                    
                        this.curLoc <- new Point(nRow, nCol)
                    
                    nCol++
                
            
            nRow++
        
        this.startLoc.copyPoint(this.curLoc)
     
    else
    
        output("Map Reading Failed. Invalid Map Detected.")
    


