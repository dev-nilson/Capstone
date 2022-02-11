using System;
using System.Collections.Generic;
using System.Text;
using static AmazingGame.GameUtilities;

namespace AmazingGame
{
    class PlayerController_GameCore
    {
        Player player;

        PlayerController_GameCore(Player p)
        {
            this.player = p;
        }

        public void PassUpdatedBoard()
        {
            if (player.Type() == Player.Tag.LOCAL)
            {

            }
            else if (GameUtilities.getGameType() == GameType.NETWORK)
            {

            }
            else if (GameUtilities.getGameType() == GameType.EASY)
            {

            }
            else // GameUtilities.getGameType() == GameType.DIFFICULT
            {

            }
        }

        public GameBoard.Coordinates GetMove()
        {
            return new GameBoard.Coordinates();
        }

        public GameBoard.Coordinates GetBuild()
        {
            return new GameBoard.Coordinates();
        }

        public GameBoard.Coordinates GetCoordinate()
        {
            if (player.Type() == Player.Tag.LOCAL)
            {
                return new GameBoard.Coordinates();
            }
            else if (GameUtilities.getGameType() == GameType.NETWORK)
            {
                return new GameBoard.Coordinates();
            }
            else if (GameUtilities.getGameType() == GameType.EASY)
            {
                return new GameBoard.Coordinates();
            }
            else // GameUtilities.getGameType() == GameType.DIFFICULT
            {
                return new GameBoard.Coordinates();
            }
        }
    }
}
