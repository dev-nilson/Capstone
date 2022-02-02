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
    public static class GameUtilities //public  ??
    {
        private static GameType gameType;

        public enum GameType
        {
            EASY,
            DIFFICULT,
            NETWORK
        }

        public static Player.Tag RandomStartingPlayer()
        {
            int num = new Random().Next(1, 3); // Generates a number [1,3)  or 1 <= num < 3
            return (Player.Tag)num; // Return the number 1 or 2 but casted as a Player
        }

        /*void swapPlayerTurn()
        {
            if (playerTurn == Player.ONE)
            {
                playerTurn = Player.TWO;
            }
            else
            {
                playerTurn = Player.ONE;
            }
        }*/

        public static void setGameType(GameType type)
        {
            gameType = type;
        }

        public static GameType getGameType()
        {
            return gameType;
        }
    }
}
