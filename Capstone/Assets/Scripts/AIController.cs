using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using static GameUtilities;
using UnityEngine;
using System.Diagnostics;

namespace AmazingGame
{
    class AIController
    {
        public static Node bestNode = null;
        public static bool isRunning = false;

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

        public static async void SimulateTurnExpert(Player opponent, Player local, GameBoard gameBoard)
        {
            isRunning = true;

            Coordinates[] pawns = Player.GetBothPlayersPawns();
            Coordinates[] localPawns = { pawns[0], pawns[1] };
            Coordinates[] opponentPawns = { pawns[2], pawns[3] };

            bool didWin = false;

            foreach (var move in gameBoard.AvailableMoves(opponentPawns[0]))
            {
                if (gameBoard.GetHeights()[move.X, move.Y] == 3)
                {
                    bestNode = new Node(opponentPawns[0], move, move);
                    didWin = true;
                    break;
                }
            }

            if (!didWin)
            {
                foreach (var move in gameBoard.AvailableMoves(opponentPawns[1]))
                {
                    if (gameBoard.GetHeights()[move.X, move.Y] == 3)
                    {
                        bestNode = new Node(opponentPawns[1], move, move);
                        didWin = true;
                        break;
                    }
                }
            }

            Node root = new Node(new Coordinates(-1, -1), new Coordinates(-1, -1), new Coordinates(-1, -1));

           
            if (!didWin)
            {
                UnityEngine.Debug.Log("Start Task");
                var result = await Task.Run(() =>
                {
                    Buffer.BlockCopy(Player.Pawns, 0, Player.PawnsCopy, 0, Player.Pawns.Length);
                    List<Node> children = GetPossiblePlaysExpert(opponent, local, opponentPawns, localPawns, gameBoard, 0);
                    return children;
                });
                root.children = result;
                UnityEngine.Debug.Log("End Task");

                bestNode = null;
                Minimax(root, gameBoard, 0, true, Double.MinValue, Double.MaxValue);
            }

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

            UnityEngine.Debug.Log("********************" + bestNode.GetMoveTo().X + "," + bestNode.GetMoveTo().Y);

            isRunning = false;
        }

        public static List<Node> GetPossiblePlays(Player playingPlayer, Player waitingPlayer, Coordinates[] playingPawns, Coordinates[] waitingPawns, GameBoard gameBoard, int turns)
        {
            if (turns == 1)
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

                                int score = NoviceAI.HeightDifference(Player.GetBothPlayersPawns(), gameBoard) +
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


        public static List<Node> GetPossiblePlaysExpert(Player playingPlayer, Player waitingPlayer, Coordinates[] playingPawns, Coordinates[] waitingPawns, GameBoard gameBoard, int turns)
        {
            if (turns == 3)
                return null;

            List<Node> possiblePlays = new List<Node>();
            foreach (var pawn in playingPawns)
            {
                List<Coordinates> moves = gameBoard.AvailableMoves(pawn);
                foreach (var move in moves)
                {
                    if (playingPlayer.updatePawnCopy(pawn, move))
                    {
                        List<Coordinates> builds = gameBoard.AvailableBuilds(move);
                        foreach (var build in builds)
                        {
                            if (gameBoard.BuildPiece(move, build))
                            {
                                Node possiblePlay = new Node(pawn, move, build);

                                int score = ExpertAI.HeightDifference(Player.GetBothPlayersPawns(), gameBoard) + ExpertAI.Centricity(playingPlayer.GetPlayerCoordinates()) +
                                ExpertAI.WinningThreat(Player.GetBothPlayersPawns(), gameBoard) + ExpertAI.Mobility(Player.GetBothPlayersPawns(), gameBoard) +
                                ExpertAI.Verticality(Player.GetBothPlayersPawns(), gameBoard);

                                possiblePlay.score = score;
                                possiblePlays.Add(possiblePlay);

                                possiblePlay.children = GetPossiblePlaysExpert(waitingPlayer, playingPlayer, waitingPawns, playingPawns, gameBoard, turns + 1);

                                gameBoard.GetHeights()[build.X, build.Y]--;
                            }
                        }
                        playingPlayer.updatePawnCopy(move, pawn);
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
                if (depth != 2 && 
                    (gameBoard.IsGameOver(new Coordinates(node.GetMoveTo().X, node.GetMoveTo().Y)) ||
                    gameBoard.CanBlock(new Coordinates(node.BuildTo().X, node.BuildTo().Y))))
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
