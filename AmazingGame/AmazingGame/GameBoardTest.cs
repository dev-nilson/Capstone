/*
 *  Author: Laura Grace Ashburn
 *  Description: This file includes implementation of the game board data and functionalities. That data includes ...
 */
using System;
using System.Collections.Generic;
using System.Text;
using GameBoard;
using GameUtilities;

namespace AmazingGame
{
    class GameBoardTest
    {
        static void Main(string[] args)
        {
            // Start
            Console.WriteLine("You are playing Pyramid Paradox!");

            //get AI (easy or difficult) or Network game


            //get starting player
            setStartingPlayer(RandomStartingPlayer());
                //if quick play, take player selection
                //if network, get? or choose?

            //initialize game board
            GameBoard board = new GameBoard();

            //starting player and secondary player place pawns

            for (int i = 0; i < 2; ++i)
            {
                //get loc for player
                if !(PlacePawn(whoseTurn(), loc)) Console.WriteLine("Error placing pawn");
                swapPlayerTurn();
            }

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