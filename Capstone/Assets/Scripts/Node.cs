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
        public Coordinates moveFrom;
        public Coordinates moveTo;
        public Coordinates buildTo;
        public List<Node> children;

        public Node(Player player, GameBoard gameBoard, Coordinates moveFrom, Coordinates moveTo, Coordinates buildTo)
        {
            this.player = player;
            this.gameBoard = gameBoard;
            this.moveFrom = moveFrom;
            this.moveTo = moveTo;
            this.buildTo = buildTo;

            this.gameBoard.MovePawn(player, moveFrom, moveTo);
            this.gameBoard.BuildPiece(moveTo, buildTo);
        }


    }
}
