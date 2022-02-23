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
         *  -   100  points if worker is at level 1.
         *  -   200  points if worker is at level 2.
         *  The combined value of the current player's workers minus the value of the opponent's workers is returned.
         */
        public static int HeightDifference(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int heightDifference = 0;

            //  TODO: Figure out heights[,] rows and columns. What goes first? What goes second?
            int playerHeight    = (gameBoard.GetHeights()[allPawns[0].X, allPawns[0].Y] + gameBoard.GetHeights()[allPawns[1].X, allPawns[1].Y]) * 100;
            int opponentHeight  = (gameBoard.GetHeights()[allPawns[2].X, allPawns[2].Y] + gameBoard.GetHeights()[allPawns[3].X, allPawns[3].Y]) * 100;

            heightDifference = opponentHeight - playerHeight;

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
        public static int Centricity(Coordinates[] playerPawns)
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
         *  -   500 points if worker is at level 3.
         *  The combined value of the current player’s workers is returned.
         */
        public static int WinningThreat(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int winningThreat = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                List<Coordinates> availableMoves;

                if (i < 2)
                {
                    if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 2)
                    {
                        availableMoves = gameBoard.AvailableMoves(allPawns[i]);

                        for (int j = 0; j < availableMoves.Count; ++j)
                        {
                            if (gameBoard.GetHeights()[availableMoves[j].X, availableMoves[j].Y] == 3)
                            {
                                winningThreat -= 100;
                            }
                        }
                    }
                }
                else
                {
                    if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 2)
                    {
                        availableMoves = gameBoard.AvailableMoves(allPawns[i]);

                        for (int j = 0; j < availableMoves.Count; ++j)
                        {
                            if (gameBoard.GetHeights()[availableMoves[j].X, availableMoves[j].Y] == 3)
                            {
                                winningThreat += 100;
                            }
                        }
                    }
                    else if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 3)
                    {
                        winningThreat += 500;
                    }
                }

            }

            return winningThreat;
        }

        /*
         *  Movility Heuristic
         *  This heuristic looks at all of the workers' positions, assigning a value of:
         *  -   0   points for each of the worker's blocked moves.
         *  -   50  points for each of the worker's available moves.
         *  The combined value of the current player's workers minus the value of the opponent's workers is returned.
         */
        public static int Mobility(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int mobility = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                //  available moves for player
                if (i < 2)
                {
                    List<Coordinates> playerAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    mobility -= playerAvailableMoves.Count;
                }
                //  available moves for opponent
                else
                {
                    List<Coordinates> opponentAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    mobility += opponentAvailableMoves.Count;
                }
            }

            mobility *= 50;

            return mobility;
        }

        /*
         *  Verticality Heuristic
         *  This heuristic looks at all of the workers' positions, assigning a value of:
         *  -   0   points for each of the worker's adjacent lower level spaces.
         *  -   1   points for each of the worker's adjacent higher or equal level spaces.
         *  The combined percentage of the current player's workers minus the percentage of the opponent's workers is returned.
         */
        public static int Verticality(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int verticality = 0;
            int playerVerticality = 0;
            int opponentVerticality = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                //  available moves for player
                if (i < 2)
                {
                    double playerVerticalMovesCount = 0;

                    List<Coordinates> playerAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    
                    foreach (var move in playerAvailableMoves)
                    {
                        if (((gameBoard.GetHeights()[move.X, move.Y]) - 1) == gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y])
                            ++playerVerticalMovesCount;
                        else if (gameBoard.GetHeights()[move.X, move.Y] == gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y])
                            playerVerticalMovesCount += 0.5;

                    }

                    if (playerAvailableMoves.Count != 0)
                    { 
                        playerVerticality += (int)Math.Round(playerVerticalMovesCount * 100.0 / playerAvailableMoves.Count);
                    }
                }
                //  available moves for opponent
                else
                {
                    double opponentVerticalMovesCount = 0;

                    List<Coordinates> opponentAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);

                    foreach (var move in opponentAvailableMoves)
                    {
                        if (((gameBoard.GetHeights()[move.X, move.Y]) - 1) == gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y])
                            ++opponentVerticalMovesCount;
                        else if (gameBoard.GetHeights()[move.X, move.Y] == gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y])
                            opponentVerticalMovesCount += 0.5;
                    }

                    if (opponentAvailableMoves.Count != 0)
                    {
                        opponentVerticality += (int)Math.Round(opponentVerticalMovesCount * 100.0 / opponentAvailableMoves.Count);
                    }
                }
            }

            verticality = opponentVerticality - playerVerticality;
            return verticality;
        }
    }
}
