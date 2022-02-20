using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class AIController
    {
        public int Minimax(Node node, int depth, bool isMaximizingPlayer)
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
