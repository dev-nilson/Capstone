﻿/*
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
    public const int BOARD_DIMENSION = 5;

    private static GameType gameType;

    private static PlayerTurn playerTurn;

    private static string P1username;
    private static string P2username;

    private static PlayerAvatar P1avatar;
    private static PlayerAvatar P2avatar;

    private static bool PlacePawnPhase;
    private static bool MovePhase;
    private static bool BuildPhase;

    private static bool PlacePawnPhase_temp;
    private static bool MovePhase_temp;
    private static bool BuildPhase_temp;

    private static bool GetFirstTile;
    private static bool GetSecondTile;

    //ADD AI DELAY VARIABLE HERE ??????????????????

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
        ONE,
        TWO
    }

    public enum MoveType
    {
        INVALID,
        VALID,
        WINNING
    }

    public enum PlayerAvatar
    {
        PHAROAH,
        SCRIBE,
        PEASANT,
        WORKER
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

    public static bool AIgame()
    {
        if (gameType == GameType.EASY || gameType == GameType.DIFFICULT)
            return true;
        else
            return false;
    }

    //////////////////////////////////////
    // Game phase functionalities
    //////////////////////////////////////
    public static void DisablePhases()
    {
        PlacePawnPhase = false;
        MovePhase = false;
        BuildPhase = false;
        GetFirstTile = false;
        GetSecondTile = false;
    }

    public static bool CanPlacePawn()
    {
        return PlacePawnPhase;
    }

    public static bool CanMove()
    {
        return MovePhase;
    }

    public static bool CanBuild()
    {
        return BuildPhase;
    }

    public static void SwapPlacePawnPhase()
    {
        PlacePawnPhase = !PlacePawnPhase;
    }

    public static void SwapMovePhase()
    {
        MovePhase = !MovePhase;
    }

    public static void SwapBuildPhase()
    {
        BuildPhase = !BuildPhase;
    }

    public static void StorePhases()
    {
        PlacePawnPhase_temp = PlacePawnPhase;
        MovePhase_temp = MovePhase;
        BuildPhase_temp = BuildPhase;
    }

    public static void RestorePhases()
    {
        PlacePawnPhase = PlacePawnPhase_temp;
        MovePhase = MovePhase_temp;
        BuildPhase = BuildPhase_temp;
    }

    public static bool WaitingForFirstTile()
    {
        return GetFirstTile;
    }

    public static bool WaitingForSecondTile()
    {
        return GetSecondTile;
    }

    public static void ReadyForTwoTiles()
    {
        GetFirstTile = true;
        GetSecondTile = false;
    }

    public static void CollectedFirstTile()
    {
        GetFirstTile = false;
        GetSecondTile = true;
    }
    public static void CollectedSecondTile()
    {
        GetSecondTile = false;
        GetFirstTile = false;
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
    // Player username functionalities
    //////////////////////////////////////
    public static void setP1username(string username)
    {
        P1username = username;
    }
    public static string getP1username()
    {
        return P1username;
    }
    public static void setP2username(string username)
    {
        P2username = username;
    }
    public static string getP2username()
    {
        return P2username;
    }
    
    //////////////////////////////////////
    // Player avatar functionalities
    //////////////////////////////////////
    public static void setP1avatar(PlayerAvatar playerAvatar)
    {
        P1avatar = playerAvatar;
    }
    public static PlayerAvatar getP1avatar()
    {
        return P1avatar;
    }
    public static void setP2avatar(PlayerAvatar playerAvatar)
    {
        P2avatar = playerAvatar;
    }
    public static PlayerAvatar getP2avatar()
    {
        return P2avatar;
    }

    // Returns a random avatar other than the one passed as a parameter
    public static PlayerAvatar RandomPlayerAvatar(PlayerAvatar avatar_A)
    {
        PlayerAvatar avatar_B;
        do
        {
            int num = new Random().Next(0, 4); // Generates a number [0,4)  or 0 <= num < 4
            avatar_B = (PlayerAvatar)num; //Return the number 0, 1, 2, or 3 but casted as a PlayerAvatar
        } while (avatar_B == avatar_A);
        return avatar_B;
    }

    //////////////////////////////////////
    // Generic game functionalities
    //////////////////////////////////////
    public static void ClearGame()
    {
        GameBoard.ClearBoard();
        Player.ClearPawns();
        // CLEAR USERNAMES AS WELL???
    }

    //////////////////////////////////////
    // Data type conversions
    //////////////////////////////////////
    // Convert list of GameBoard.Coordinates to binary 5x5 grid
    // 0 is valid, -1 is invalid
    public static int[,] ConvertToBinaryBoard(List<Coordinates> locs)
    {
        int size = BOARD_DIMENSION;

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