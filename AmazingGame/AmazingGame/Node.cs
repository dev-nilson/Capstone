using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class Node
    {
        public int score = 0;
        public List<Node> children;

        public Node(int score)
        {
            this.score = score;
        }
    }
}
