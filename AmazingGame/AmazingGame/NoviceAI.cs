using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class NoviceAI
    {
        // TODO: Replace ints for Coordinates
        /*
         *  Height Difference Heuristic
         *  This heuristic looks at the all of the workers’ positions, assigning a value of:
         *  -   0   points if the builder is at level 0.
         *  -   10  points if the builder is at level 1.
         *  -   20  points if the builder is at level 2.
         *  The combined value of the current player’s workers minus the value of the opponents workers is returned.
         */
        public int HeightDifference(int currentWorker1X, int currentWorker1Y, int currentWorker2X, int currentWorker2Y, int opponentWorker1X, int opponentWorker1Y, int opponentWorker2X, int opponentWorker2Y)
        {
            int heightDifference = 0;

            // TODO: Figure out heights[,] rows and columns. What goes first? What goes second?
            int currentPlayerHeight  = (heights[currentWorker1X, currentWorker1Y] + heights[currentWorker2X, currentWorker2Y]) * 10;
            int opponentPlayerHeight = (heights[opponentWorker1X, opponentWorker1Y] + heights[opponentWorker2X, opponentWorker2Y]) * 10;

            heightDifference = currentPlayerHeight - opponentPlayerHeight;

            return heightDifference;
        }

    }
}
