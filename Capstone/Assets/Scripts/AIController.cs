using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AmazingGame
{
    class AIController
    {
        public static Node bestNode = null;       

        public static Coordinates PlacePawns(GameBoard gameBoard)
        {
            int x = 2;
            int y = 2;

            if (gameBoard.IsOccupied(new Coordinates(x, y)))
            {
                System.Random random = new System.Random();
                x = random.Next(1, 4);
                y = random.Next(1, 4);
            }

            return new Coordinates(x, y);
        }

        public static void SimulateTurn(Player opponent, Player local, GameBoard gameBoard)
        {
            Coordinates[] pawns = Player.GetBothPlayersPawns();
            Coordinates[] localPawns = { pawns[0], pawns[1] };
            Coordinates[] opponentPawns = { pawns[2], pawns[3] };

            Node root = new Node(new Coordinates(-1, -1), new Coordinates(-1, -1), new Coordinates(-1, -1));
            root.children = GetPossiblePlays(opponent, local, opponentPawns, localPawns, gameBoard, 0);

            bestNode = null;
            Minimax(root, gameBoard, 0, true, Double.MinValue, Double.MaxValue);

            if (bestNode == null)
            {
                foreach (var child in root.children)
                {
                    if (root.score == child.score)
                    {
                        bestNode = child;
                        break;
                    }
                }
            }
        }

        public static List<Node> GetPossiblePlays(Player playingPlayer, Player waitingPlayer, Coordinates[] playingPawns, Coordinates[] waitingPawns, GameBoard gameBoard, int turns)
        {
            if (turns == 3)
                return null;

            List<Node> possiblePlays = new List<Node>();
            foreach (var pawn in playingPawns)
            {
                List<Coordinates> moves = gameBoard.AvailableMoves(pawn);
                foreach (var move in moves)
                {
                    if (playingPlayer.updatePawn(pawn, move))
                    {
                        List<Coordinates> builds = gameBoard.AvailableBuilds(move);
                        foreach (var build in builds)
                        {
                            if (gameBoard.BuildPiece(move, build))
                            {
                                Node possiblePlay = new Node(pawn, move, build);

                                int score = NoviceAI.HeightDifference(Player.GetBothPlayersPawns(), gameBoard) + NoviceAI.Centricity(playingPlayer.GetPlayerCoordinates()) +
                                NoviceAI.WinningThreat(Player.GetBothPlayersPawns(), gameBoard) + NoviceAI.Mobility(Player.GetBothPlayersPawns(), gameBoard) + 
                                NoviceAI.Verticality(Player.GetBothPlayersPawns(), gameBoard);

                                possiblePlay.score = score;
                                possiblePlays.Add(possiblePlay);
                                
                                possiblePlay.children = GetPossiblePlays(waitingPlayer, playingPlayer, waitingPawns, playingPawns, gameBoard, turns + 1);

                                gameBoard.GetHeights()[build.X, build.Y]--;
                            }
                        }
                        playingPlayer.updatePawn(move, pawn);
                    }
                }
            }

            return possiblePlays;
        }

       

        public static int Minimax(Node node, GameBoard gameBoard, int depth, bool isMaximizingPlayer)
        {
            if (node.GetMoveTo().X != -1 && node.GetMoveTo().Y != -1)
            {
                if (depth != 2 && gameBoard.IsGameOver(new Coordinates(node.GetMoveTo().X, node.GetMoveTo().Y)))
                {
                    bestNode = node;
                    return node.score;
                }
            }

            if (node.children == null)
                return node.score;

            if (isMaximizingPlayer)
            {
                node.score = Int16.MinValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, gameBoard, depth + 1, false);
                    node.score = Math.Max(node.score, value);
                }

                return node.score;
            }
            else
            {
                node.score = Int16.MaxValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, gameBoard, depth + 1, true);
                    node.score = Math.Min(node.score, value);
                }

                return node.score;
            }
        }

        public static int Minimax(Node node, GameBoard gameBoard, int depth, bool isMaximizingPlayer, double alpha, double beta)
        {
            if (node.GetMoveTo().X != -1 && node.GetMoveTo().Y != -1)
            {
                if (depth != 2 && gameBoard.IsGameOver(new Coordinates(node.GetMoveTo().X, node.GetMoveTo().Y)))
                {
                    bestNode = node;
                    return node.score;
                }
            }

            if (node.children == null)
                return node.score;

            if (isMaximizingPlayer)
            {
                node.score = Int16.MinValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, gameBoard, depth + 1, false, alpha, beta);
                    node.score = Math.Max(node.score, value);
                    alpha = Math.Max(alpha, node.score);

                    if (beta <= alpha) break;
                }

                return node.score;
            }
            else
            {
                node.score = Int16.MaxValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, gameBoard, depth + 1, true, alpha, beta);
                    node.score = Math.Min(node.score, value);
                    beta = Math.Min(beta, node.score);

                    if (beta <= alpha) break;
                }

                return node.score;
            }
        }
    }
}
