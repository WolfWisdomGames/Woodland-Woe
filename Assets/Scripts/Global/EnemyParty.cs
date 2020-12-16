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
        partyMembers.Add(new CharacterSheet("Bramble", 1, "CombatAvatarBramble"));
        partyMembers.Add(new CharacterSheet("Bramble", 1, "CombatAvatarBramble"));
        partyMembers.Add(new CharacterSheet("Bramble", 1, "CombatAvatarBramble"));
        partyMembers.Add(new CharacterSheet("Bramble", 1, "CombatAvatarBramble"));
        rng = new System.Random();
    }

    public static void SpawnPartyMembers()
    {
        float xPos = 13f;
        float yPos = -.75f;
        Quaternion facing = new Quaternion(0f, 180f, 0f, 0f);
        foreach (CharacterSheet c in partyMembers)
        {
            if (c.CanDeploy())
            {
                c.SpawnCombatAvatar(new Vector3(xPos, yPos, 0f), facing, true);
                xPos += -1f;
                yPos -= .75f;
            }
        }
    }
}
