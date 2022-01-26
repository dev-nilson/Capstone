/*
 *  Author: Laura Grace Ashburn
 *  Description: This file includes implementation of the game board data and functionalities. That data includes ...
 */
using System;
using System.Collections.Generic;
using System.Text;
using AmazingGame;

namespace AmazingGame
{
    class GameBoardTest
    {
        static void Main(string[] args)
        {
            // Start
            Console.WriteLine("You are playing Pyramid Paradox!");

            //Initialize P1;
            Console.WriteLine("P1's username: ");
            string username = Console.ReadLine();
            bool first = true;
            bool local = true;

            Player P1 = new Player(local, username, first);

            //Initialize P2;
            Console.WriteLine("P2's username: ");
            username = Console.ReadLine();
            first = false;
            local = false;

            Player P2 = new Player(local, username, first);

            //initialize game board
            GameBoard board = new GameBoard();

            //starting player and secondary player place pawns

            //place starting pawns
            Console.WriteLine("P1s move?\n Enter X and Y (1-5)");
            int x = Convert.ToInt32(Console.ReadLine());
            int y = Convert.ToInt32(Console.ReadLine());
            GameBoard.Coordinates loc = new GameBoard.Coordinates(x - 1, y - 1);
            if (!board.PlacePawn(P1,loc)) Console.WriteLine("Error placing P1's pawn");

            

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
        }
    }
}