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

        public void InitializeBoard(bool resize=true)
        {
            if (resize) heights = new int[BOARD_DIMENSION, BOARD_DIMENSION];

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

                if (whoseTurn == Player.ONE)
                {
                    if (PlayerOnePawns[0] != new Coordinates()) // If first pawn is not already placed
                    {
                        //place pawn
                    }
                    else if (PlayerOnePawns[1] != new Coordinates()) // If second pawn is not already placed
                    {
                        //place pawn
                    }
                    else // If both pawns are already placed, no new pawn can be placed
                    {
                        //return false
                    }
                }
                else if (whoseTurn == Player.TWO)
                {
                    if (PlayerTwoPawns[0] != new Coordinates())
                    {
                        //place pawn
                    }
                    else if (PlayerTwoPawns[1] != new Coordinates())
                    {
                        //place pawn
                    }
                    else
                    {
                        //return false
                    }
                }
                else //ERROR -- NO PLAYER'S TURN
            }
            else // return false (space is occupied or out of bounds)
        }

        MoveType ValidateMove(Coordinates curLoc, Coordinates newLoc)
        {
            //check if move in bounds ??
            //if new space is unoccupied and current space is occupied
            if (IsInBounds(curLoc) && IsOccupied(curLoc) && IsInBounds(newLoc) && !IsOccupied(newLoc))
            {
                //check if move is within one space of pawn's location
                if (abs(newLoc.X - curLoc.X) == 1 || abs(newLoc.Y - curLoc.Y) == 1)
                {
                    //if new height - current height <= 1
                    if (heights[newLoc.X, newLoc.Y] - heights[curLoc.X, curLoc.Y] <= 1) // CREATE FUNCTION TO RETURN HEIGHT AT A SPECIFIC COORDINATE ?????????
                    {
                        // If moving to a 3-tiered tower, the game is won!
                        if (heights[newLoc.X, newLoc.Y] == 3)
                        {
                            return MoveType.WINNING;
                        }
                        else
                        {
                            return MoveType.VALID;
                        }
                    }
                }
            }
            return MoveType.INVALID;
        }

        bool ValidateBuild(Coordinates curLoc, Coordinates newLoc)
        {
            //check if move in bounds ??
            //if new space is unoccupied and current space is occupied
            if (IsInBounds(curLoc) && IsOccupied(curLoc) && IsInBounds(newLoc) && !IsOccupied(newLoc))
            {
                //check if move is within one space of pawn's location
                if (abs(newLoc.X - curLoc.X) == 1 || abs(newLoc.Y - curLoc.Y) == 1)
                {
                    //if new height < 4
                    if (heights[newLoc.X, newLoc.Y] < 4)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool MovePawn(Coordinates curLoc, Coordinates newLoc)
        {
            //call validateMove inside here ?? if so, have a return false
            MoveType move = ValidateMove(curLoc, newLoc);
            if (move == MoveType.INVALID || move == MoveType.WINNING)
            {
                return false;
            }

            // the move is valid
            else
            {
                //update pawn location
                //return true
            }
            //ADD THROW EXCEPT TO THIS ???
        }

        bool BuildPiece(Coordinates curLoc, Coordinates newLoc)
        {
            if (ValidateBuild(curLoc, newLoc))
            {
                heights[newLoc.X, newLoc.Y] += 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        void ClearBoard()
        {
            Coordinates[] PlayerOnePawns = new Coordinates[2];
            Coordinates[] PlayerTwoPawns = new Coordinates[2];
            InitializeBoard(false);
        }
    }
}
