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
        TurnManager m = GameObject.FindObjectOfType<TurnManager>();
        int xPos = 6;
        int yPos = 7;
        Quaternion facing = new Quaternion(0f, 180f, 0f, 0f);
        foreach (CharacterSheet c in partyMembers)
        {
            if (c.CanDeploy())
            {
                CombatController avatar = c.SpawnCombatAvatar(new Vector3(xPos + yPos, xPos * 0.75f - yPos * 0.75f, 0f), facing, true);
                avatar.SetCurrentTile(m.tileGrid[xPos, yPos]);
                xPos -= 1;
            }
        }
    }
}
