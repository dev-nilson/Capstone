/*
 *  Author: Laura Grace Ashburn
 *  Description: This file includes implementation of the game board data and functionalities. That data includes ...
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static AmazingGame.GameUtilities;

namespace AmazingGame
{
    class GameBoardTest
    {
        static void Main(string[] args)
        {
            // Start
            Console.WriteLine("You are playing Pyramid Paradox!\n");

            //Initialize P1;
            Console.WriteLine("P1's username: ");
            string username = Console.ReadLine();
            PlayerTurn playerTurn = RandomStartingPlayer();
            bool local = true;

            Player P1 = new Player(local, username);

            //Initialize P2;
            Console.WriteLine("P2's username: ");
            username = Console.ReadLine();
            local = false;

            Player P2 = new Player(local, username);

            //initialize game board
            GameBoard board = new GameBoard();
            bool success = true;
            int x, y;
            GameBoard.Coordinates loc = new GameBoard.Coordinates();
            //starting player and secondary player place pawns


            //P1 place starting pawns
            do
            {
                Console.WriteLine("\nP1's move? Enter X and Y (0-4)");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());
                loc = new GameBoard.Coordinates(x, y);

                success = board.PlacePawn(P1, loc);
                if (!success) Console.WriteLine("Error placing P1's pawn");
            } while (!success);
            DisplayPawns(P1,P2);
            SwapPlayerTurn();

            //P2 place starting pawns
            do
            {
                Console.WriteLine("\nP2's move? Enter X and Y (0-4)");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());
                loc = new GameBoard.Coordinates(x, y);

                success = board.PlacePawn(P2, loc);
                if (!success) Console.WriteLine("Error placing P2's pawn");
            } while (!success); 
            DisplayPawns(P1,P2);

            //P2 place starting pawns
            do
            {
                Console.WriteLine("\nP2's move? Enter X and Y (0-4)");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());
                loc = new GameBoard.Coordinates(x, y);

                success = board.PlacePawn(P2, loc);
                if (!success) Console.WriteLine("Error placing P2's pawn");
            } while (!success);
            DisplayPawns(P1,P2);
            SwapPlayerTurn();

            //P1 place starting pawns
            do
            {
                Console.WriteLine("\nP1's move? Enter X and Y (0-4)");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());
                loc = new GameBoard.Coordinates(x, y);

                success = board.PlacePawn(P1, loc);
                if (!success) Console.WriteLine("Error placing P1's pawn");
            } while (!success);
            DisplayPawns(P1,P2);


            /////////////////////////////
            int newx, newy;
            GameBoard.MoveType status;
            /////////////////////////////

            //Time for P1's move
            Console.WriteLine("\nTime for P1's move! Enter X and Y (0-4)");
            GameBoard.Coordinates[] pawnLocs = P1.GetPlayerCoordinates();
            do
            {
                // Determine which pawn is going to be moved. Or, if no moves are available, the player loses

                if (P1.HasNoMoves(board))
                {
                    Console.WriteLine("P1 loses because there are no available moves :(");
                    break;
                }
                // Else if pawn_0 has no available moves
                else if (board.AvailableMoves(pawnLocs[0]).Count == 0)
                {
                    Console.WriteLine("You can move your pawn at " + pawnLocs[1].X + "," + pawnLocs[1].Y + "!");
                    x = pawnLocs[1].X;
                    y = pawnLocs[1].Y;
                }
                // Else if pawn_1 has no available moves
                else if (board.AvailableMoves(pawnLocs[1]).Count == 0)
                {
                    Console.WriteLine("You can move your pawn at " + pawnLocs[0].X + "," + pawnLocs[0].Y + "!");
                    x = pawnLocs[0].X;
                    y = pawnLocs[0].Y;
                }
                // Else both pawns have available moves
                else
                {
                    Console.WriteLine("Which pawn would you like to move? " + pawnLocs[0].X + "," + pawnLocs[0].Y + " or " + pawnLocs[1].X + "," + pawnLocs[1].Y + "?");
                    x = Convert.ToInt32(Console.ReadLine());
                    y = Convert.ToInt32(Console.ReadLine());
                }
                

                List<GameBoard.Coordinates> moves = board.AvailableMoves(new GameBoard.Coordinates(x, y));
                Console.Write("\nWhere would you like to move it to?");
                
                for (int i = 0; i < moves.Count; ++i)
                {
                    Console.Write("  " + moves[i].X + "," + moves[i].Y);
                } Console.WriteLine();
                newx = Convert.ToInt32(Console.ReadLine());
                newy = Convert.ToInt32(Console.ReadLine());
                status = board.MovePawn(P1, new GameBoard.Coordinates(x, y), new GameBoard.Coordinates(newx, newy));
                if (status == GameBoard.MoveType.INVALID) Console.WriteLine("Error moving P1's pawn");
                else if (status == GameBoard.MoveType.WINNING) Console.WriteLine("You won!");
            } while (status == GameBoard.MoveType.INVALID);
            DisplayPawns(P1, P2);
            DisplayHeights(board);

            //P1's first build
            Console.WriteLine("P1's first build! Enter X and Y (0-4)");
            pawnLocs = P1.GetPlayerCoordinates();
            do
            {
                Console.WriteLine("Which pawn would you like to build from?  " + pawnLocs[0].X + "," + pawnLocs[0].Y + " or " + pawnLocs[1].X + "," + pawnLocs[1].Y + "?");
                x = Convert.ToInt32(Console.ReadLine());
                y = Convert.ToInt32(Console.ReadLine());

                List<GameBoard.Coordinates> builds = board.AvailableBuilds(new GameBoard.Coordinates(x, y));
                Console.Write("\nWhere would you like to build?");
                for (int i = 0; i < builds.Count; ++i)
                {
                    Console.Write("  " + builds[i].X + "," + builds[i].Y);
                }
                Console.WriteLine();
                newx = Convert.ToInt32(Console.ReadLine());
                newy = Convert.ToInt32(Console.ReadLine());
                success = board.BuildPiece(new GameBoard.Coordinates(x, y), new GameBoard.Coordinates(newx, newy));
                if (!success) Console.WriteLine("Error building piece");
            } while (!success);
            DisplayPawns(P1, P2);
            DisplayHeights(board);

            SwapPlayerTurn();

            /*
            //get curLoc and newLoc
            while (ValidateMove(curLoc, newLoc) != MoveType.WINNING)
            {
                while (ValidateMove(curLoc, newLoc) == MoveType.INVALID)
                {
                    //get curLoc and newLoc
                }
                if (MovePawn(curLoc, newLoc))
                {
                    //get curloc and newloc
                    if (ValidateBuild(curLoc, newLoc))
                    {
                        if (!BuildPiece(curLoc, newLoc)) Console.WriteLine("Error building piece");
                        else swapPlayerTurn();
                    }
                    else Console.WriteLine("Move not valid");
                }
                else Console.WriteLine("Error moving pawn");

                //get curLoc and newLoc
            }
            //while not winning
            //move pawn
            //build tower piece
            //swap player
            //if restart? or exit game?

            Console.WriteLine("The winner is ", whoseTurn(), "!!!");

            //game over
            */
        }

        static void DisplayPawns(Player P1, Player P2)
        {
            GameBoard.Coordinates[] P1pawns = P1.GetPlayerCoordinates();
            GameBoard.Coordinates[] P2pawns = P2.GetPlayerCoordinates();
            
            Console.WriteLine("\n-PAWNS-");
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    GameBoard.Coordinates loc = new GameBoard.Coordinates(i, j);
                    if (P1pawns.Contains(loc))
                    {
                        Console.Write("1");
                    }
                    else if (P2pawns.Contains(loc))
                    {
                        Console.Write("2");
                    }
                    else Console.Write("o");
                }
                Console.Write("\n");
            }
        }

        /*static void DisplayPawns(Player P1, Player P2, List<GameBoard.Coordinates>)
        {

        }*/

        static void DisplayHeights(GameBoard board)
        {
            Console.WriteLine("\n-HEIGHTS-");
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    Console.Write(board.heights[i,j]);
                } Console.WriteLine();
            }
        }
    }
}