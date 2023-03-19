using System;

namespace src {
    public class Route {
        private Point parent;
        private Route[] children;
        /* Maksimum 4 elements, index of:
        0 is UpChildren
        1 is LeftChildren
        2 is RightChildren
        3 is DownChildren
        */
        
            // ctor
        public Route() {
            this.parent = new Point();
            this.children = new Route[4] {null, null, null, null};
        }

        public Route(Point p){
            this.parent = new Point(p);
            this.children = new Route[4] {null, null, null, null};
        }

        // setter getter
        public void setParent(Point p){
            this.parent.copyPoint(p);
        }
        public void setUpChild(Point p){
            this.children[0] = new Route();
            this.children[0].setParent(p);
        }
        public void setLeftChild(Point p){
            this.children[1] = new Route();
            this.children[1].setParent(p);
        }
        public void setRightChild(Point p){
            this.children[2] = new Route();
            this.children[2].setParent(p);
        }
        public void setDownChild(Point p){
            this.children[3] = new Route();
            this.children[3].setParent(p);
        }

        public Point getParent(){
            return this.parent;
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
            int count = 0;
            if (getNChild() == 0){
                return 1;
            } else {
                for (int i = 0; i < getNChild(); i ++){
                    if (children[i] != null){
                        count += children[i].getNodesAmount();
                    }
                }
                return count;
            }
        }
    }


}