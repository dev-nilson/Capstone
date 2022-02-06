/*
 * Author: Laura Grace Ashburn
 * Description: ...
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Coordinates
{
    public int X { get; } //{ get; init; }
    public int Y { get; } //{ get; init; }

    public Coordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Coordinates()
    {
        X = -1; //initialized to -1 for PlacePawn function *** ?? restructure ??
        Y = -1;
    }

    public override bool Equals(object loc)
    {
        return Equals(loc as Coordinates);
    }

    public bool Equals(Coordinates loc)
    {
        if (loc is null) return false;
        else return this.X == loc.X && this.Y == loc.Y;
    }

    public static bool operator ==(Coordinates loc1, Coordinates loc2)
    {
        if (loc1.X == loc2.X && loc1.Y == loc2.Y) return true;
        else return false;
    }

    public static bool operator !=(Coordinates loc1, Coordinates loc2) => !(loc1 == loc2);
}