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
        //////////////////////////////////////
        // Variable & Types
        //////////////////////////////////////
        private static GameType gameType;

        public enum GameType
        {
            EASY,
            DIFFICULT,
            NETWORK
        }

        private static PlayerTurn playerTurn;

        public enum PlayerTurn
        {
            ONE = 1,
            TWO = 2
        }

        //////////////////////////////////////
        // Game type functionalities
        //////////////////////////////////////
        public static void setGameType(GameType type)
        {
            gameType = type;
        }

        public static GameType getGameType()
        {
            return gameType;
        }

        //////////////////////////////////////
        // Player turn functionalities
        //////////////////////////////////////
        public static void SetPlayerTurn(PlayerTurn turn)
        {
            playerTurn = turn;
        }

        public static PlayerTurn GetPlayerTurn()
        {
            return playerTurn;
        }

        public static PlayerTurn RandomStartingPlayer()
        {
            int num = new Random().Next(1, 3); // Generates a number [1,3)  or 1 <= num < 3
            playerTurn = (PlayerTurn)num; //Return the number 1 or 2 but casted as a Player
            return playerTurn;
        }

        public static void SwapPlayerTurn()
        {
            if (playerTurn == PlayerTurn.ONE) playerTurn = PlayerTurn.TWO;
            else playerTurn = PlayerTurn.ONE;
        }
    }
}
