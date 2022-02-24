using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class Node
    {
        public int score;
        public GameBoard gameBoard;
        public GameBoard.Coordinates moveFrom;
        public GameBoard.Coordinates moveTo;
        public GameBoard.Coordinates buildTo;
        public List<Node> children = null;

        public Node(GameBoard gameBoard, GameBoard.Coordinates moveFrom, GameBoard.Coordinates moveTo, GameBoard.Coordinates buildTo)
        {
            this.score = 0;
            this.gameBoard = gameBoard;
            this.moveFrom = moveFrom;
            this.moveTo = moveTo;
            this.buildTo = buildTo;
        }


    }
}
