using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class AIController
    {
        public static Node chosenTurn = null;

        public static int Minimax(Node node, int depth, bool isMaximizingPlayer)
        {
            if (node.children == null)
                return node.score;

            if (isMaximizingPlayer)
            {
                node.score = Int16.MinValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, depth + 1, false);
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
                    int value = Minimax(n, depth + 1, true);
                    node.score = Math.Min(node.score, value);
                }

                return node.score;
            }
        }

        public static void SimulateTurn(Player opponent, GameBoard gameBoard)
        {
            int counter = 0;
            List<Node> possibleTurns = new List<Node>();

            Coordinates[] pawns = Player.GetBothPlayersPawns();
            foreach (var pawn in pawns)
            {
                //  gameBoard instance
                List<Coordinates> moves = gameBoard.AvailableMoves(pawn);

                foreach (var move in moves)
                {
                    if (counter >= 2)
                    {
                        if (opponent.updatePawn(pawn, move))
                        {
                            List<Coordinates> builds = gameBoard.AvailableBuilds(move);
                            foreach (var build in builds)
                            {
                                Node node = new Node(gameBoard, pawn, move, build);

                                gameBoard.GetHeights()[build.X, build.Y] += 1;

                                //  Heuristic functions
                                node.score += NoviceAI.HeightDifference(Player.GetBothPlayersPawns(), gameBoard);
                                node.score += NoviceAI.Centricity(opponent.GetPlayerCoordinates());
                                node.score += NoviceAI.WinningThreat(Player.GetBothPlayersPawns(), gameBoard);
                                node.score += NoviceAI.Mobility(Player.GetBothPlayersPawns(), gameBoard);
                                node.score += NoviceAI.Verticality(Player.GetBothPlayersPawns(), gameBoard);

                                //  Undo built piece
                                gameBoard.GetHeights()[build.X, build.Y] -= 1;

                                possibleTurns.Add(node);
                            }

                            //  Undo moved pawn
                            opponent.updatePawn(move, pawn);
                        }
                    }

                }

                ++counter;
            }

            Node root = new Node();
            root.children = possibleTurns;

            Minimax(root, 1, true);

            foreach (var child in root.children)
            {
                if (root.score == child.score)
                {
                    chosenTurn = child;
                    break;
                }
            }
        }

        public int Minimax(Node node, int depth, bool isMaximizingPlayer, double alpha, double beta)
        {
            if (node.children == null) return node.score;

            if (isMaximizingPlayer)
            {
                node.score = Int16.MinValue;

                for (int i = 0; i < node.children.Count; i++)
                {
                    Node n = node.children[i];
                    int value = Minimax(n, depth + 1, false, alpha, beta);
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
                    int value = Minimax(n, depth + 1, true, alpha, beta);
                    node.score = Math.Min(node.score, value);
                    beta = Math.Min(beta, node.score);

                    if (beta <= alpha) break;
                }

                return node.score;
            }
        }
    }
}
