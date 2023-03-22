using System

namespace src 
class Nodes 
Nodes parent
Point node
Nodes[] children
/* Maksimum 4 elements, index of:
0 is UpChildren
1 is LeftChildren
2 is RightChildren
3 is DownChildren
*/

    // ctor
Nodes() 
    this.node <- new Point()
    this.parent <- null
    this.children <- new Nodes[4] null, null, null, null


Nodes(Point p)
    this.node <- new Point(p)
    this.parent <- null
    this.children <- new Nodes[4] null, null, null, null


// setter getter
procedure setNode(Point p)
    this.node.copyPoint(p)

procedure setParent(Nodes n)
    this.parent <- n

procedure setUpChild(Point p)
    this.children[0] <- new Nodes()
    this.children[0].setNode(p)
    this.children[0].setParent(this)

procedure setLeftChild(Point p)
    this.children[1] <- new Nodes()
    this.children[1].setNode(p)
    this.children[1].setParent(this)

procedure setRightChild(Point p)
    this.children[2] <- new Nodes()
    this.children[2].setNode(p)
    this.children[2].setParent(this)

procedure setDownChild(Point p)
    this.children[3] <- new Nodes()
    this.children[3].setNode(p)
    this.children[3].setParent(this)


Nodes getParent()
    -> this.parent

Point getNode()
    -> this.node

Nodes[] getChildren()
    -> this.children


Nodes getUpChild()
    -> this.children[0]


Nodes getLeftChild()
    -> this.children[1]


Nodes getRightChild()
    -> this.children[2]


Nodes getDownChild()
    -> this.children[3]


// other methods
int getNChild()
    int count <- 0
    foreach(Nodes n in this.children)
        if (n !<- null)
            count++
        
    
    -> count


int getNodesAmount()
    int count <- 1
    foreach(Nodes newN in children)
        if(newN !<- null)
            count +<- newN.getNodesAmount()
        
    
    -> count


// print and display
procedure displayRoutes(int h, int depth)
    for (int j <- 0 j < h*depth j++)
            output(' ')
    
    getNode().displayPoint() 
    foreach(Nodes childN in getChildren())
        if (childN !<- null)
            childN.displayRoutes(h, depth+1)
        
    



