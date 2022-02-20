using System;
using System.Collections.Generic;
using System.Text;

namespace AmazingGame
{
    class RandomAI
    {
        public static GameBoard.Coordinates MoveRandomly(GameBoard.Coordinates[] playerPawns, GameBoard gameBoard)
        {
            GameBoard.Coordinates coordinates;
            Random random = new Random();
            int pawnIndex = -1;

            //  both workers have available moves
            if (gameBoard.AvailableMoves(playerPawns[0]).Count > 0 && gameBoard.AvailableMoves(playerPawns[1]).Count > 0)
                pawnIndex = random.Next(0, 2);
            //  only one worker has available moves
            else if (gameBoard.AvailableMoves(playerPawns[0]).Count > 0)
                pawnIndex = 0;
            //  only one worker has available moves
            else if (gameBoard.AvailableMoves(playerPawns[1]).Count > 0)
                pawnIndex = 1;

            List<GameBoard.Coordinates> availableMoves = gameBoard.AvailableMoves(playerPawns[pawnIndex]);
            int coordinatesIndex = random.Next(0, availableMoves.Count);
            coordinates = availableMoves[coordinatesIndex];

            return coordinates;
        }

        public static GameBoard.Coordinates BuildRandomly(GameBoard.Coordinates playerPawn, GameBoard gameBoard)
        {
            GameBoard.Coordinates coordinates;

            Random random = new Random();
            List<GameBoard.Coordinates> availableBuilds = gameBoard.AvailableBuilds(playerPawn);
            int coordinatesIndex = random.Next(0, availableBuilds.Count);
            coordinates = availableBuilds[coordinatesIndex];

            return coordinates;
        }
    }
}
