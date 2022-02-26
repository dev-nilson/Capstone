using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class Node
    {
        public int score;
        public GameBoard gameBoard;
        private Coordinates moveFrom;
        private Coordinates moveTo;
        private Coordinates buildTo;
        public List<Node> children = null;

        public Node(GameBoard gameBoard = null, Coordinates moveFrom = null, Coordinates moveTo = null, Coordinates buildTo = null)
        {
            this.gameBoard = gameBoard;
            this.moveFrom = moveFrom;
            this.moveTo = moveTo;
            this.buildTo = buildTo;
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
