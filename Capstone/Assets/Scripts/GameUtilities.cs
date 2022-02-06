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

public static class GameUtilities //public  ??
{
    //////////////////////////////////////
    // Variables
    //////////////////////////////////////
    private static GameType gameType;

    private static PlayerTurn playerTurn;

    //////////////////////////////////////
    // Data types
    //////////////////////////////////////
    public enum GameType
    {
        EASY,
        DIFFICULT,
        NETWORK
    }

    public enum PlayerTurn
    {
        ONE = 1,
        TWO = 2
    }

    public enum MoveType
    {
        INVALID,
        VALID,
        WINNING
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

    //////////////////////////////////////
    // Data type conversions
    //////////////////////////////////////
    // Convert list of GameBoard.Coordinates to binary 5x5 grid
    // 0 is valid, -1 is invalid
    public static int[,] ConvertToBinaryBoard(List<Coordinates> locs)
    {
        int size = GameBoard.BOARD_DIMENSION;

        int[,] board = new int[size, size];

        for (int x = 0; x < size; ++x)
        {
            for (int y = 0; y < size; ++y)
            {
                if (locs.Contains(new Coordinates(x,y)))
                {
                    board[x, y] = 0;
                }
                else
                {
                    board[x, y] = -1;
                }
            }
        }

        return board;
    }
}