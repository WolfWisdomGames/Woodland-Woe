using UnityEngine;
using System.Collections;

public class EnemyController : CombatController
{

    override public bool IsEnemy()
    {
        return true;
    }

    override protected bool ContainsEnemy(Tile tile)
    {
        if (tile.occupant == null) return false;
        return tile.occupant.IsPC();
    }

    void Update()
    {
        if (!isTurn) return;
        if (isActing) return;
        Tile choice = AIChooseMove();
        if (choice == null)
        {
            EndTurn();
        }
        else
        {
            if (choice.occupant != null)
            {
                selectedAction.BeginAction(choice);
            }
            else
            {
                Action move = GetComponent<ActionMove>();
                move.BeginAction(choice);
            }
        }
    }

    // Picks an arbitrary attackable target.
    GameObject PickTarget()
    {
        return manager.PickArbitraryPC();
    }

    Tile AIChooseMove()
    {
        float bestScore = 0.0f;
        Tile bestChoice = null;
        GameObject target = PickTarget();
        foreach (Tile option in selectableTiles)
        {
            if (EvaluateMove(option, target) > bestScore)
            {
                bestScore = EvaluateMove(option, target);
                bestChoice = option;
            }
        }
        if (bestScore > 0.0f) return bestChoice;
        return null;
    }

    // If the tile can be attacked, returns 100. Otherwise,
    // returns 50 minus its distance from the target.
    // AI also prefers high ground.
    float EvaluateMove(Tile tile, GameObject target)
    {
        if (ContainsEnemy(tile))
        {
            return 100.0f;
        }
        return 50.0f - (Vector3.Distance(tile.transform.position, target.transform.position));
    }
}
