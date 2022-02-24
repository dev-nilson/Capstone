﻿using System;
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
        public static int HeightDifference(GameBoard.Coordinates[] allPawns, GameBoard gameBoard)
        {
            int heightDifference = 0;

            //  TODO: Figure out heights[,] rows and columns. What goes first? What goes second?
            int playerHeight    = (gameBoard.heights[allPawns[0].X, allPawns[0].Y] + gameBoard.heights[allPawns[1].X, allPawns[1].Y]) * 100;
            int opponentHeight  = (gameBoard.heights[allPawns[2].X, allPawns[2].Y] + gameBoard.heights[allPawns[3].X, allPawns[3].Y]) * 100;

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
        public static int Centricity(GameBoard.Coordinates[] playerPawns)
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
        public static int WinningThreat(GameBoard.Coordinates[] allPawns, GameBoard gameBoard)
        {
            int winningThreat = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                List<GameBoard.Coordinates> availableMoves;

                if (i < 2)
                {
                    if (gameBoard.heights[allPawns[i].X, allPawns[i].Y] == 2)
                    {
                        availableMoves = gameBoard.AvailableMoves(allPawns[i]);

                        for (int j = 0; j < availableMoves.Count; ++j)
                        {
                            if (gameBoard.heights[availableMoves[j].X, availableMoves[j].Y] == 3)
                            {
                                winningThreat -= 100;
                            }
                        }
                    }
                }
                else
                {
                    if (gameBoard.heights[allPawns[i].X, allPawns[i].Y] == 2)
                    {
                        availableMoves = gameBoard.AvailableMoves(allPawns[i]);

                        for (int j = 0; j < availableMoves.Count; ++j)
                        {
                            if (gameBoard.heights[availableMoves[j].X, availableMoves[j].Y] == 3)
                            {
                                winningThreat += 100;
                            }
                        }
                    }
                    if (gameBoard.heights[allPawns[i].X, allPawns[i].Y] == 3)
                    {
                        winningThreat += 1000;
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
        public static int Mobility(GameBoard.Coordinates[] allPawns, GameBoard gameBoard)
        {
            int mobility = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                //  available moves for player
                if (i < 2)
                {
                    List<GameBoard.Coordinates> playerAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    mobility -= playerAvailableMoves.Count;
                }
                //  available moves for opponent
                else
                {
                    List<GameBoard.Coordinates> opponentAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
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
        public static int Verticality(GameBoard.Coordinates[] allPawns, GameBoard gameBoard)
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

                    List<GameBoard.Coordinates> playerAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    
                    foreach (var move in playerAvailableMoves)
                    {
                        if (((gameBoard.heights[move.X, move.Y]) - 1) == gameBoard.heights[allPawns[i].X, allPawns[i].Y])
                            ++playerVerticalMovesCount;
                        else if (gameBoard.heights[move.X, move.Y] == gameBoard.heights[allPawns[i].X, allPawns[i].Y])
                            playerVerticalMovesCount += 0.5;

                    }

                    playerVerticality += (int)Math.Round(playerVerticalMovesCount * 100.0 / playerAvailableMoves.Count);
                }
                //  available moves for opponent
                else
                {
                    double opponentVerticalMovesCount = 0;

                    List<GameBoard.Coordinates> opponentAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);

                    foreach (var move in opponentAvailableMoves)
                    {
                        if (((gameBoard.heights[move.X, move.Y]) - 1) == gameBoard.heights[allPawns[i].X, allPawns[i].Y])
                            ++opponentVerticalMovesCount;
                        else if (gameBoard.heights[move.X, move.Y] == gameBoard.heights[allPawns[i].X, allPawns[i].Y])
                            opponentVerticalMovesCount += 0.5;
                    }

                    opponentVerticality += (int)Math.Round(opponentVerticalMovesCount * 100.0 / opponentAvailableMoves.Count);
                }
            }

            verticality = opponentVerticality - playerVerticality;
            return verticality;
        }
    }
}
