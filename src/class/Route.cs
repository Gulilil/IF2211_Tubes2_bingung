using System;

namespace src {
    public class Route {
        private Route parent;
        private Point node;
        private Route[] children;
        /* Maksimum 4 elements, index of:
        0 is UpChildren
        1 is LeftChildren
        2 is RightChildren
        3 is DownChildren
        */
        
            // ctor
        public Route() {
            this.node = new Point();
            this.parent = null;
            this.children = new Route[4] {null, null, null, null};
        }

        public Route(Point p){
            this.node = new Point(p);
            this.parent = null;
            this.children = new Route[4] {null, null, null, null};
        }

        // setter getter
        public void setNode(Point p){
            this.node.copyPoint(p);
        }
        public void setParent(Route r){
            this.parent = r;
        }
        public void setUpChild(Point p){
            this.children[0] = new Route();
            this.children[0].setNode(p);
            this.children[0].setParent(this);
        }
        public void setLeftChild(Point p){
            this.children[1] = new Route();
            this.children[1].setNode(p);
            this.children[1].setParent(this);
        }
        public void setRightChild(Point p){
            this.children[2] = new Route();
            this.children[2].setNode(p);
            this.children[2].setParent(this);
        }
        public void setDownChild(Point p){
            this.children[3] = new Route();
            this.children[3].setNode(p);
            this.children[3].setParent(this);
        }

        public Route getParent(){
            return this.parent;
        }
        public Point getNode(){
            return this.node;
        }
        public Route[] getChildren(){
            return this.children;
        }

        public Route getUpChild(){
            return this.children[0];
        }

        public Route getLeftChild(){
            return this.children[1];
        }

        public Route getRightChild(){
            return this.children[2];
        }

        public Route getDownChild(){
            return this.children[3];
        }

        // other methods
        public int getNChild(){
            int count = 0;
            foreach(Route r in this.children){
                if (r != null){
                    count++;
                }
            }
            return count;
        }

        public int getNodesAmount(){
            int count = 1;
            foreach(Route newR in children){
                if(newR != null){
                    count += newR.getNodesAmount();
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
            foreach(Route childR in getChildren()){
                if (childR != null){
                    childR.displayRoutes(h, depth+1);
                }
            }
        }
    }
}
