using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class NoviceAI
    {
        /*
         *  Height Difference Heuristic
         *  This heuristic looks at all of the workers' positions, assigning a value of:
         *  -   0   points if worker is at level 0.
         *  -   10  points if worker is at level 1.
         *  -   20  points if worker is at level 2.
         *  The combined value of the current player's workers minus the value of the opponent's workers is returned.
         */
        public int HeightDifference(GameBoard.Coordinates[] allPawns)
        {
            int heightDifference = 0;

            //  TODO: Figure out heights[,] rows and columns. What goes first? What goes second?
            int playerHeight    = (GameBoard.heights[allPawns[0].X, allPawns[0].Y] + GameBoard.heights[allPawns[1].X, allPawns[1].Y]) * 10;
            int opponentHeight  = (GameBoard.heights[allPawns[2].X, allPawns[2].Y] + GameBoard.heights[allPawns[3].X, allPawns[3].Y]) * 10;

            heightDifference = playerHeight - opponentHeight;

            return heightDifference;
        }

        /*
         *  Centricity Heuristic
         *  This heuristic looks at all of the player's workers' positions, assigning a value of:
         *  -   0   points if worker is in the border spaces.
         *  -   5   points if worker is in the inner 3x3 ring.
         *  -   10  points if worker is in the middle space.
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

        /*  
         *  Winning Threat Heuristic
         *  This heuristic looks at all of the player's workers' positions at level 2, assigning a value of:
         *  -   0   points if worker has no adjacent level 3 tiles.
         *  -   100 points for each of the worker's adjacent level 3 tiles.
         *  The combined value of the current player’s workers is returned.
         */
        public int WinningThreat(GameBoard.Coordinates[] playerPawns)
        {
            int winningThreat = 0;

            for (int i = 0; i < playerPawns.Length; ++i)
            {
                List<Coordinates> availableMoves;

                if (GameBoard.heights(playerPawns[i].X, playerPawns[i].Y) == 2)
                {
                    availableMoves = GameBoard.AvailableMoves(playerPawns[i].X, playerPawns[i].Y);
                }

                for (int j = 0; j < availableMoves.Count; ++j)
                {
                    if (GameBoard.heights(availableMoves[j].X, availableMoves[j].Y) == 3)
                    {
                        winningThreat += 100;
                    }
                }
            }

            return winningThreat;
        }

        /*
         *  Movility Heuristic
         *  This heuristic looks at all of the workers' positions, assigning a value of:
         *  -   0   points for each of the worker's blocked moves.
         *  -   5   points for each of the worker's available moves.
         *  The combined value of the current player's workers minus the value of the opponent's workers is returned.
         */
        public int Mobility(GameBoard.Coordinates[] allPawns)
        {
            int mobility = 0;
            List<Coordinates> playerAvailableMoves;
            List<Coordinates> opponentAvailableMoves;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                if (i < 2)
                {
                    playerAvailableMoves = GameBoard.AvailableMoves(allPawns[i].X, allPawns[i].Y);
                    mobility += playerAvailableMoves.Count;
                }
                else
                {
                    opponentAvailableMoves = GameBoard.AvailableMoves(allPawns[i].X, allPawns[i].Y);
                    mobility -= opponentAvailableMoves.Count;
                }
            }

            mobility *= 5;

            return mobility;
        }
    }
}
