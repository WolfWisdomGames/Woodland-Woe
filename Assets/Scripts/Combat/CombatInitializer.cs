﻿using UnityEngine;
using System.Collections;

// There should only ever be one CombatInitializer per combat scene.
public class CombatInitializer : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] TurnManager manager;

    // Use this for initialization
    void Start()
    {
        // Populate Tile Grid.
        manager.tileGrid = new Tile[Constants.COMBAT_WIDTH, Constants.COMBAT_HEIGHT];
        for (int x = 0; x < Constants.COMBAT_WIDTH; x++)
        {
            for (int y = 0; y < Constants.COMBAT_HEIGHT; y++)
            {
                GameObject newTile = Instantiate(tilePrefab, new Vector3(x + y, x * 0.75f - y * 0.75f, 0), Quaternion.identity);
                SpriteRenderer rend = newTile.GetComponent<SpriteRenderer>();
                rend.sortingOrder = y - x;
                Tile t = newTile.GetComponent<Tile>();
                manager.tileGrid[x, y] = t;
                t.x = x; t.y = y;
            }
        }
        EnemyParty.SpawnPartyMembers();
        PlayerParty.SpawnPartyMembers();

        manager.combatants.Sort(new SortCombatants());
    }

}
