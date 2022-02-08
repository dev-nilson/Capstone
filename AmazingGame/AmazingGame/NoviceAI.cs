using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class NoviceAI
    {
        /*
         *  Height Difference Heuristic
         *  This heuristic looks at the all of the workers’ positions, assigning a value of:
         *  -   0   points if the builder is at level 0.
         *  -   10  points if the builder is at level 1.
         *  -   20  points if the builder is at level 2.
         *  The combined value of the current player’s workers minus the value of the opponents workers is returned.
         */
        public int HeightDifference(GameBoard.Coordinates[] allPawns)
        {
            int heightDifference = 0;

            //  TODO: Figure out heights[,] rows and columns. What goes first? What goes second?
            int currentPlayerHeight  = (GameBoard.heights[allPawns[0].X, allPawns[0].Y] + GameBoard.heights[allPawns[1].X, allPawns[1].Y]) * 10;
            int opponentPlayerHeight = (GameBoard.heights[allPawns[2].X, allPawns[2].Y] + GameBoard.heights[allPawns[3].X, allPawns[3].Y]) * 10;

            heightDifference = currentPlayerHeight - opponentPlayerHeight;

            return heightDifference;
        }

        /*
         *  Centricity Heuristic
         *  This heuristic looks at the all of the workers’ positions, assigning a value of:
         *  -   0   points if the builder is in the border spaces.
         *  -   5   points if the builder is in the inner 3x3 ring.
         *  -   10  points if the builder is in the middle space.
         *  The combined value of the current player’s workers is returned.
         */
        public int Centricity(GameBoard.Coordinates[] playerPawns)
        {
            int centricity = 0;

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ X ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 1) || (playerPawns[1].X == 1 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ X ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 2) || (playerPawns[1].X == 1 && playerPawns[1].Y == 2))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ X ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 3) || (playerPawns[1].X == 1 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ X ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 1) || (playerPawns[1].X == 2 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ X ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 2) || (playerPawns[1].X == 2 && playerPawns[1].Y == 2))
            {
                centricity += 10;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ X ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 3) || (playerPawns[1].X == 2 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ X ■ ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 1) || (playerPawns[1].X == 3 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ X ■ ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 2) || (playerPawns[1].X == 3 && playerPawns[1].Y == 2))
            {
                centricity += 5;
            }

            /*  
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ ■ ■
             *  ■ ■ ■ X ■
             *  ■ ■ ■ ■ ■
             */
            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 3) || (playerPawns[1].X == 3 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            return centricity;
        }


        public int WinningThreat(int currentWorker1X, int currentWorker1Y, int currentWorker2X, int currentWorker2Y)
        {
            int winningThreat = 0;

            

            return winningThreat;
        }
    }
}
