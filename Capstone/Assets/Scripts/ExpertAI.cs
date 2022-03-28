using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class ExpertAI
    {
        public static int HeightDifference(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int playerHeight = (gameBoard.GetHeights()[allPawns[0].X, allPawns[0].Y] + gameBoard.GetHeights()[allPawns[1].X, allPawns[1].Y]) * 100;
            int opponentHeight = (gameBoard.GetHeights()[allPawns[2].X, allPawns[2].Y] + gameBoard.GetHeights()[allPawns[3].X, allPawns[3].Y]) * 100;

            int heightDifference = opponentHeight - playerHeight;
            return heightDifference;
        }

        public static int Centricity(Coordinates[] playerPawns)
        {
            int centricity = 0;

            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 1) || (playerPawns[1].X == 1 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 2) || (playerPawns[1].X == 1 && playerPawns[1].Y == 2))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 1 && playerPawns[0].Y == 3) || (playerPawns[1].X == 1 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 1) || (playerPawns[1].X == 2 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 2) || (playerPawns[1].X == 2 && playerPawns[1].Y == 2))
            {
                centricity += 10;
            }

            if ((playerPawns[0].X == 2 && playerPawns[0].Y == 3) || (playerPawns[1].X == 2 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 1) || (playerPawns[1].X == 3 && playerPawns[1].Y == 1))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 2) || (playerPawns[1].X == 3 && playerPawns[1].Y == 2))
            {
                centricity += 5;
            }

            if ((playerPawns[0].X == 3 && playerPawns[0].Y == 3) || (playerPawns[1].X == 3 && playerPawns[1].Y == 3))
            {
                centricity += 5;
            }

            return centricity * 5;
        }

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
                                winningThreat -= 500;
                            }
                        }
                    }
                    else if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 3)
                    {
                        winningThreat -= 10000;
                    }
                }
                else
                {
                    if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 2)
                    {
                        availableMoves = gameBoard.AvailableMoves(allPawns[i]);

                        foreach (var move in availableMoves)
                        {
                            if (gameBoard.GetHeights()[move.X, move.Y] == 3)
                            {
                                winningThreat += 500;
                            }
                        }
                    }
                    else if (gameBoard.GetHeights()[allPawns[i].X, allPawns[i].Y] == 3)
                    {
                        winningThreat += 10000;
                    }
                }

            }

            return winningThreat;
        }

        public static int Mobility(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int mobility = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
                if (i < 2)
                {
                    List<Coordinates> playerAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    mobility -= playerAvailableMoves.Count;
                }
                else
                {
                    List<Coordinates> opponentAvailableMoves = gameBoard.AvailableMoves(allPawns[i]);
                    mobility += opponentAvailableMoves.Count;
                }
            }

            mobility *= 25;

            return mobility;
        }

        public static int Verticality(Coordinates[] allPawns, GameBoard gameBoard)
        {
            int playerVerticality = 0;
            int opponentVerticality = 0;

            for (int i = 0; i < allPawns.Length; ++i)
            {
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

            int verticality = opponentVerticality - playerVerticality;
            return verticality;
        }
    }
}
