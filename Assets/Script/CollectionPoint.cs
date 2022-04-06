using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionPoint
{
    public static int cherry=0;
    public static int gem=0;
    public static int level;

    private static int lastCherry;
    private static int lastGem;
    public static void ResetPoint()
    {
        cherry = lastCherry;
        gem = lastGem;
    }
    public static void UpdatePoints()
    {
        lastCherry = cherry;
        lastGem = gem;
    }
}
