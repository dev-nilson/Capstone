using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    public class Player
    {
        public enum Tag {
            LOCAL,
            OPPONENT
        }

        Tag player;

        string username;

        private bool myTurn;

        // Stores locations of the player's two pawns
        // Local player gets [0,] and opposing player gets [1,]
        private static GameBoard.Coordinates[][] Pawns = { new GameBoard.Coordinates[] { new GameBoard.Coordinates(), new GameBoard.Coordinates() },
                                                           new GameBoard.Coordinates[] { new GameBoard.Coordinates(), new GameBoard.Coordinates() } };



        //constructor w/ input for Local/Opponent, username, myTurn
        public Player(bool local, string _name, bool _turn)
        {
            if (local) player = Tag.LOCAL;
            else player = Tag.OPPONENT;

            username = _name;
            myTurn = _turn;
        }

        public static GameBoard.Coordinates[] GetBothPlayersPawns()
        {
            GameBoard.Coordinates[] allPawns = { Pawns[0][0], Pawns[0][1], Pawns[1][0], Pawns[1][1] };
            return allPawns;
        }

        // Returns an array containing the two pawn coordinates for a player
        public GameBoard.Coordinates[] GetPlayerCoordinates() // ************************************** DOES THIS GET USED?
        {
            return Pawns[(int)this.player];
        }

        public bool addNewPawn(GameBoard.Coordinates loc)
        {
            if (Pawns[(int)this.player][0] == new GameBoard.Coordinates()) // If first pawn is not already placed
            {
                //place pawn
                Pawns[(int)this.player][0] = loc;
                return true;
            }
            else if (Pawns[(int)this.player][1] == new GameBoard.Coordinates()) // If second pawn is not already placed
            {
                //place pawn
                Pawns[(int)this.player][1] = loc;
                return true;
            }
            else // If both pawns are already placed, no new pawn can be placed
            {
                return false;
            }
        }

        public bool updatePawn(GameBoard.Coordinates curLoc, GameBoard.Coordinates newLoc)
        {
            if (Pawns[(int)this.player][0] == curLoc) // If moving the first pawn
            {
                //update pawn
                Pawns[(int)this.player][0] = newLoc;
                return true;
            }
            else if (Pawns[(int)this.player][1] == curLoc) // If moving the second pawn
            {
                //update pawn
                Pawns[(int)this.player][1] = newLoc;
                return true;
            }
            else // If neither pawn was selected
            {
                return false;
            }
        }

        public bool isMyTurn()
        {
            return this.myTurn;
        }

        public void changeTurn()
        {
            this.myTurn = !this.myTurn;
        }
    }
}
