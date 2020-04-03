using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
  wave,
  endless
}

public static class Global
{
    //Turns out the "glitch" that this was supposed to fix doesn't exist, but I'll leave this in incase we
    private static Vector3 direction;
    public static int level;
    public static GameMode mode;
    public static Vector3 GetDirection()
    {
        return direction;
    }
}
