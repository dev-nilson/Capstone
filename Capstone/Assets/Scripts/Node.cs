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
        private Coordinates moveFrom;
        private Coordinates moveTo;
        private Coordinates buildTo;
        public List<Node> children = new List<Node>();

        public Node(Player player = null, GameBoard gameBoard = null, Coordinates moveFrom = null, Coordinates moveTo = null, Coordinates buildTo = null)
        {
            this.player = player;
            this.gameBoard = gameBoard;
            this.moveFrom = moveFrom;
            this.moveTo = moveTo;
            this.buildTo = buildTo;

            if (player != null)
            {
                this.gameBoard.BuildPiece(moveTo, buildTo);
            }
        }

        public Coordinates GetMoveFrom()
        {
            return moveFrom;
        }

        public Coordinates GetMoveTo()
        {
            return moveTo;
        }

        public Coordinates BuildTo()
        {
            return buildTo;
        }
    }
}
