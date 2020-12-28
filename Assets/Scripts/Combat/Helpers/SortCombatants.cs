using System.Collections.Generic;
using UnityEngine;

public class SortCombatants : IComparer<CombatController>
{
    public int Compare(CombatController x, CombatController y)
    {
        CharacterSheet xSheet = x?.characterSheet;
        CharacterSheet ySheet = y?.characterSheet;
        return ySheet.finesse.CompareTo(xSheet.finesse);
    }
}
