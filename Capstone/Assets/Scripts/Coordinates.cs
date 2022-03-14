﻿/*
 * Author: Laura Grace Ashburn
 * Description: ...
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class Coordinates
{
    public int X; //{ get; init; }
    public int Y; //{ get; init; }

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
        if (this is null && loc is null) return true;
        else if (this is null || loc is null) return false;
        else return this.X == loc.X && this.Y == loc.Y;
    }

    public static bool operator ==(Coordinates loc1, Coordinates loc2)
    {
        if (loc1 is null && loc2 is null) return true;
        else if (loc1 is null || loc2 is null) return false;
        else return loc1.X == loc2.X && loc1.Y == loc2.Y;
    }

    public static bool operator !=(Coordinates loc1, Coordinates loc2) => !(loc1 == loc2);

    // Serialization function for use with Photon
    public static byte[] Serialize(object coordinates)
    {
        var temp = JsonUtility.ToJson(coordinates);
        Debug.Log($"I'm going to send {temp}");
        return System.Text.Encoding.ASCII.GetBytes(temp);
    }

    // Deserialization function for use with Photon.
    public static Coordinates Deserialize(byte[] data)
    {
        var temp = System.Text.Encoding.ASCII.GetString(data);
        Debug.Log($"I recieved {temp}.");
        return JsonUtility.FromJson<Coordinates>(temp);
    }
}