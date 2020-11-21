using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerParty
{
    public static List<CharacterSheet> partyMembers;
    private static System.Random rng;

    public static void Reset()
    {
        partyMembers = new List<CharacterSheet> { };
        partyMembers.Add(new CharacterSheet("Sam", 2));
        partyMembers.Add(new CharacterSheet("Friendly Woe", 1));
        rng = new System.Random();
    }
}
