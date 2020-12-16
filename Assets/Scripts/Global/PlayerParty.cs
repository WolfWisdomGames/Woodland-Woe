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
        partyMembers.Add(new CharacterSheet("Sam", 2, "CombatAvatarSam"));
        partyMembers.Add(new CharacterSheet("Friendly Woe", 1, "CombatAvatarWoe"));
        partyMembers.Add(new CharacterSheet("Friendly Woe", 1, "CombatAvatarWoe"));
        rng = new System.Random();
    }

    public static void SpawnPartyMembers()
    {
        float xPos = 1f;
        float yPos = .75f;
        Quaternion facing = new Quaternion(0f, 180f, 0f, 0f);
        foreach (CharacterSheet c in partyMembers)
        {
            if (c.CanDeploy())
            {
                c.SpawnCombatAvatar(new Vector3(xPos, yPos, 0f), facing, true);
                xPos += 1f;
                yPos += .75f;
            }
        }
    }
}
