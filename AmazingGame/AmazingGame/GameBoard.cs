/*
 *  Author: Laura Grace Ashburn
 *  Description: This file includes implementation of the game board data and functionalities. That data includes ...
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

/* GAMEBOARD
 * - 5x5
 * 
 * Function to check for valid moves, win conditions
 * 
 * Function to move pieces
 * 
 * Function to check for valid new builds
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
    public class GameBoard //public  ??
    {
        const int BOARD_DIMENSION = 5;
        
        int[,] heights; // 2D element the size of the game board, stores height of each tile: 0, 1, 2, 3, or 4        //private ??

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

        // Returns whether a pawn exists in the board coordinate
        bool IsOccupied(Coordinates loc)
        {
            // Get a list of pawns (four total between both players)
            Coordinates[] allPawns = Player.GetBothPlayersPawns();
            if (allPawns.Contains(loc))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool PlacePawn(Player player, Coordinates loc)
        {
            //if space is in bounds and unoccupied
            if (IsInBounds(loc) && !IsOccupied(loc))
            {
                if (player.isMyTurn())
                {
                    if (player.addNewPawn(loc))
                    {
                        return true;
                    }
                    else // If both pawns are already placed, no new pawn can be placed
                    {
                        return false;
                    }
                }
                else
                { //not my turn
                    return false;
                }
            }
            else
            {
                return false; //space is occupied or out of bounds
            }
        }

        bool MovePawn(Player player, Coordinates curLoc, Coordinates newLoc)
        {
            //call validateMove inside here ?? if so, have a return false
            MoveType move = ValidateMove(curLoc, newLoc);
            if (move == MoveType.INVALID || move == MoveType.WINNING)
            {
                return false;
            }
            else // the move is valid
            {
                if (player.isMyTurn())
                {
                    if (player.updatePawn(curLoc, newLoc)) //update pawn location
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else //not my turn
                {
                    return false;
                }
            }
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





        // DON'T MAKE PUBLIC ????????????????????




        MoveType ValidateMove(Coordinates curLoc, Coordinates newLoc)
        {
            //check if move in bounds ??
            //if new space is unoccupied and current space is occupied
            if (IsInBounds(curLoc) && IsOccupied(curLoc) && IsInBounds(newLoc) && !IsOccupied(newLoc))
            {
                //check if move is within one space of pawn's location
                if (Math.Abs(newLoc.X - curLoc.X) == 1 || Math.Abs(newLoc.Y - curLoc.Y) == 1)
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
                if (Math.Abs(newLoc.X - curLoc.X) == 1 || Math.Abs(newLoc.Y - curLoc.Y) == 1)
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
    }
}
