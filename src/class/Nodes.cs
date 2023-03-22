using System;

namespace src {
    public class Nodes {
        private Nodes parent;
        private Point node;
        private Nodes[] children;
        /* Maksimum 4 elements, index of:
        0 is UpChildren
        1 is LeftChildren
        2 is RightChildren
        3 is DownChildren
        */
        
            // ctor
        public Nodes() {
            this.node = new Point();
            this.parent = null;
            this.children = new Nodes[4] {null, null, null, null};
        }

        public Nodes(Point p){
            this.node = new Point(p);
            this.parent = null;
            this.children = new Nodes[4] {null, null, null, null};
        }

        // setter getter
        public void setNode(Point p){
            this.node.copyPoint(p);
        }
        public void setParent(Nodes n){
            this.parent = n;
        }
        public void setUpChild(Point p){
            this.children[0] = new Nodes();
            this.children[0].setNode(p);
            this.children[0].setParent(this);
        }
        public void setLeftChild(Point p){
            this.children[1] = new Nodes();
            this.children[1].setNode(p);
            this.children[1].setParent(this);
        }
        public void setRightChild(Point p){
            this.children[2] = new Nodes();
            this.children[2].setNode(p);
            this.children[2].setParent(this);
        }
        public void setDownChild(Point p){
            this.children[3] = new Nodes();
            this.children[3].setNode(p);
            this.children[3].setParent(this);
        }

        public Nodes getParent(){
            return this.parent;
        }
        public Point getNode(){
            return this.node;
        }
        public Nodes[] getChildren(){
            return this.children;
        }

        public Nodes getUpChild(){
            return this.children[0];
        }

        public Nodes getLeftChild(){
            return this.children[1];
        }

        public Nodes getRightChild(){
            return this.children[2];
        }

        public Nodes getDownChild(){
            return this.children[3];
        }

        // other methods
        public int getNChild(){
            int count = 0;
            foreach(Nodes n in this.children){
                if (n != null){
                    count++;
                }
            }
            return count;
        }

        public int getNodesAmount(){
            int count = 1;
            foreach(Nodes newN in children){
                if(newN != null){
                    count += newN.getNodesAmount();
                }
            }
            return count;
        }

        // print and display
        public void displayRoutes(int h, int depth){
            for (int j = 0; j < h*depth; j++){
                 System.Console.Write(' ');
            }
            getNode().displayPoint(); System.Console.WriteLine();
            foreach(Nodes childN in getChildren()){
                if (childN != null){
                    childN.displayRoutes(h, depth+1);
                }
            }
        }
    }
}
