using System;
using System.Collections.Generic;
using System.Text;
using static AmazingGame.GameUtilities;

namespace AmazingGame
{
    class PlayerController
    {
        Player player;

        PlayerController(Player p)
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

        }

        public GameBoard.Coordinates GetBuild()
        {

        }

        public GameBoard.Coordinates GetCoordinate()
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
    }
}
