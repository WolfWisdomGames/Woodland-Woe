using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyParty
{
    public static List<CharacterSheet> partyMembers;
    private static System.Random rng;

    public static void Reset()
    {
        partyMembers = new List<CharacterSheet> { };
        partyMembers.Add(new CharacterSheet("Bramble", 1));
        partyMembers.Add(new CharacterSheet("Bramble", 1));
        partyMembers.Add(new CharacterSheet("Bramble", 1));
        partyMembers.Add(new CharacterSheet("Bramble", 1));
        rng = new System.Random();
    }

    public static void SpawnPartyMembers()
    {

    }
}
