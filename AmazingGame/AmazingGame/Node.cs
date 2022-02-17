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
        public GameBoard.Coordinates buildTo;
        public List<Node> children;

        public Node(Player player, GameBoard gameBoard, GameBoard.Coordinates moveFrom, GameBoard.Coordinates moveTo, GameBoard.Coordinates buildTo)
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
