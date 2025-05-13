using UnityEngine;


public enum Faction { Player, Enemy};

public static class FactionHelper
{

    public static bool IsHostile(Faction faction1, Faction faction2)
    {
        return faction1 != faction2;
    }

    public static bool IsFriend(Faction faction1, Faction faction2)
    {
        return faction1 == faction2;
    }

}




