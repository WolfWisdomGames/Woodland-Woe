using UnityEngine;
using System.Collections;

public class EnemyController : CombatController
{

    // Use this for initialization
    void Start()
    {

    }

    override public bool IsEnemy()
    {
        return true;
    }

    override protected bool ContainsEnemy(Tile tile)
    {
        if (tile.occupant == null) return false;
        return tile.occupant.IsPC();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
