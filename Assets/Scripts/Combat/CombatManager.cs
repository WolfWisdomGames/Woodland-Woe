using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class CombatManager
{
    public static Tile[,] tileGrid;
    public static List<CombatController> combatants = new List<CombatController>();
}
