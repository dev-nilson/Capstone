/*
 *  Author: Laura Grace Ashburn
 *  Description:
 */
using System;
using System.Collections.Generic;
using System.Text;

/* GAME UTILITIES
 * 
 */

namespace AmazingGame
{
    public class GameUtilities //public  ??
    {
        private Player playerTurn;

        private GameType gameType;

        enum Player
        {
            ONE,
            TWO
        }

        enum GameType
        {
            EASY,
            DIFFICULT,
            NETWORK
        }

        Player RandomStartingPlayer()
        {
            int num = new Random().Next(1, 3); // Generates a number [1,3)  or 1 <= num < 3
            return (Player)num; // Return the number 1 or 2 but casted as a Player
        }

        void setStartingPlayer(Player starting)
        {
            playerTurn = starting;
        }

        Player whoseTurn()
        {
            return playerTurn;
        }

        void swapPlayerTurn()
        {
            if (playerTurn == Player.ONE)
            {
                playerTurn = Player.TWO;
            }
            else
            {
                playerTurn = Player.ONE;
            }
        }

        void setGameType(GameType type)
        {
            gameType = type;
        }

        GameType getGameType()
        {
            return gameType;
        }
    }
}
