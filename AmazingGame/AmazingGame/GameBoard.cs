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

        private Coordinates[] PlayerOnePawns = new Coordinates[2]; // Stores locations of Player 1's two pawns
        private Coordinates[] PlayerTwoPawns = new Coordinates[2]; // Stores locations of Player 2's two pawns

        enum MoveType
        {
            INVALID,
            VALID,
            WINNING
        }

        enum Player // *********************** DUPLICATED IN GAMEUTILITIES.CPP
        {
            ONE,
            TWO
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
        public GameBoard()
        {
            InitializeBoard();
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

        bool IsInBounds(Coordinates loc)
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

        // Returns an array containing the two pawn coordinates for a player
        Coordinates[] GetPlayerCoordinates(Player num) // ************************************** DOES THIS GET USED?
        {
            if (num == Player.ONE)
            {
                return PlayerOnePawns;
            }
            else
            {
                return PlayerTwoPawns;
            }
        }

        // Returns whether a pawn exists in the board coordinate
        bool IsOccupied(Coordinates loc)
        {
            if (loc == PlayerOnePawns[0] ||
                loc == PlayerOnePawns[1] ||
                loc == PlayerTwoPawns[0] ||
                loc == PlayerTwoPawns[1])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //verify player turn ??????????????????????????

        bool PlacePawn(Player whoseTurn, Coordinates loc)  // NEEDS WORK ********************************************************************************
        {
            //if space is in bounds and unoccupied
            if (IsInBounds(loc) && !IsOccupied(loc))
            {
                Coordinates[] playerPawns;

                //get player's pawn coordinates
                if (whoseTurn == Player.ONE)    // && PlayerOnePawns[0] != new Coordinates() && PlayerTwoPawns[1] != new Coordinates())
                {
                    playerPawns = PlayerOnePawns;
                }
                else
                {
                    playerPawns = PlayerTwoPawns;
                }

                //if player's pawns are not both already placed
                if (playerPawns[0] != new Coordinates())
                {
                    //place pawn
                }
                else if (playerPawns[1] != new Coordinates())
                {
                    //place pawn
                }
                else
                {
                    //return false
                }
            }
            //else
            //return false
        }

        MoveType ValidateMove()
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

        bool MovePawn()
        {
            //call validateMove inside here ?? if so, have a return false

            //get current location, get new location
            //fill new location with pawn
            //old pawn location marked as unoccupied
            //return true
            //ADD THROW EXCEPT TO THIS ???
        }

        bool BuildPiece()
        {
            //get location
            //if location unoccupied by pawns
                //if location height > 3
                    //return false
                //if location height <= 3
                    //location height incremented by 1
                    //return true

        }

        void ClearBoard()
        {

        }
    }
}
