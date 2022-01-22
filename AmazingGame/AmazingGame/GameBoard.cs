/*
 *  Author: Laura Grace Ashburn
 *  Description: This file includes implementation of the game board data and functionalities. That data includes ...
 */
using System;
using System.Collections.Generic;
using System.Text;

/* GAMEBOARD
 * - 5x5
 * 
 * Function to check for valid moves, win conditions
 * 
 * Function to move pieces
 * 
 * Function to check for valid new builds
 * 
 * 4 pawns
 * - player 1 or 2
 * 
 * 
 * Game start:
 * - place pieces (different from move pieces??)
 * 
 * Game end:
 * - clear board/data
 * 
 * 
 */

namespace AmazingGame
{
    class GameBoard //public  ??
    {
        const int BOARD_DIMENSION = 5;
        
        int[,] heights; // 2D element the size of the game board, stores height of each tile: 0, 1, 2, 3, or 4        //private ??

        Coordinates[] PlayerOnePawns = new Coordinates[2]; // Stores locations of Player 1's two pawns
        Coordinates[] PlayerTwoPawns = new Coordinates[2]; // Stores locations of Player 2's two pawns

        private Player Turn; // Store player turn, either ONE or TWO

        public enum Player
        {
            ONE,
            TWO
        }

        enum MoveType
        {
            INVALID,
            VALID,
            WINNING
        }

        public class Coordinates //public ??
        {
            public int X { get; } //{ get; init; }
            public int Y { get; } //{ get; init; }

            public Coordinates(int x, int y)
            {
                X = x;
                Y = y;
            }

            public Coordinates()
            {
                X = -1; //initialized to -1 for PlacePawn function *** ?? restructure ??
                Y = -1;
            }
        }
        
        // Constructor that initializes game board with heights of 0
        public GameBoard(Player StartingPlayer)
        {
            InitializeBoard();
        }

        public GameBoard()
        {
            Player StartingPlayer = RandomStartingPlayer();
            InitializeBoard();
        }

        Player RandomStartingPlayer()
        {
            int num = new Random().Next(1,3); // Generates a number [1,3)  or 1 <= num < 3
            return (Player)num; // Return the number 1 or 2 but casted as a Player
        }

        public void InitializeBoard()
        {
            heights = new int[BOARD_DIMENSION, BOARD_DIMENSION];

            for (int x = 0; x < 5; ++x)
            {
                for (int y = 0; y < 5; ++y)
                {
                    heights[x, y] = 0;
                }
            }
        }

        bool isInBounds(Coordinates loc)
        {
            if (loc.X < 0 || loc.X >= BOARD_DIMENSION)
            {
                return false;
            }
            if (loc.Y < 0 || loc.Y >= BOARD_DIMENSION)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        bool isOccupied(Coordinates loc)
        {
            //if loc in PlayerOnePawns
                //return true
            //if loc in PlayerTwoPawns
                //return true
            //else
                //return false
        }

        //verify player turn ??

        bool placePawn(Player whoseTurn, Coordinates loc)
        {
            //get move
            //if space is in bounds and unoccupied and not placed both pawns already
                //place pawn
                //return true
            //else
                //return false
        }

        MoveType validateMove()
        {
            //get pawn location, get new move

            //check if move in bounds ??
            //check if move is within one space of pawn's location
                //abs(newLoc.X - curLoc.X) == 1 || abs(newLoc.Y - curLoc.Y) == 1
            //if new space is unoccupied and current space is occupied
            //if new height - current height <= 1
            //if new height equals 3
            //return MoveType.Winning
            //else
            //return MoveType.Valid
            //else
            //return MoveType.Invalid
        }

        bool movePawn()
        {
            //call validateMove inside here ?? if so, have a return false

            //get current location, get new location
            //fill new location with pawn
            //old pawn location marked as unoccupied
            //return true
            //ADD THROW EXCEPT TO THIS ???
        }

        bool buildPiece()
        {
            //get location
            //if location unoccupied by pawns
                //if location height > 3
                    //return false
                //if location height <= 3
                    //location height incremented by 1
                    //return true

        }

        void clearBoard()
        {

        }
    }
}
