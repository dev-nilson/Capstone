/*
 *  Author: Laura Grace Ashburn
 *  Description: ...
 */
using System;
using System.Collections.Generic;
using System.Text;
using static GameUtilities;

class PlayerController_GameCore
{
    Player player;

    PlayerController_GameCore(Player p)
    {
        this.player = p;
    }

    /*
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
    */

    public Coordinates GetMove()
    {
        if (player.Type() == Player.Tag.LOCAL)
        {
            
            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {

            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {

            return new Coordinates();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {

            return new Coordinates();
        }
    }

    public Coordinates GetBuild()
    {
        
        return new Coordinates();
    }

    public Coordinates GetCoordinate()
    {
        if (player.Type() == Player.Tag.LOCAL)
        {

            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.NETWORK)
        {

            return new Coordinates();
        }
        else if (GameUtilities.getGameType() == GameType.EASY)
        {

            return new Coordinates();
        }
        else // GameUtilities.getGameType() == GameType.DIFFICULT
        {

            return new Coordinates();
        }
    }
}
