using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class Node
    {
        public int score;
        public Player player;
        public GameBoard gameBoard;
        public GameBoard.Coordinates moveFrom;
        public GameBoard.Coordinates moveTo;
        public GameBoard.Coordinates buildFrom;
        public GameBoard.Coordinates buildTo;
        public List<Node> children;

       
    }
}
