using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This covers the mechanics for basic attacks, and special moves that include an attack should inherit from it.
public class ActionBasicAttack : ActionMove
{

    override public ActionType ACTION_TYPE { get { return ActionType.ATTACK; } }

    override protected void Start()
    {
        // Save one tile at the end of the movement path:
        // this is the tile containing the target enemy.
        reserveTiles = 1;
        base.Start();
    }

    virtual protected float ATTACK_DURATION { get { return 1.0f; } }

    // Update is called once per frame
    override protected void Update()
    {
        if (!inProgress)
        {
            return;
        }
        if (currentPhase == Phase.MOVING)
        {
            Move();
        }
        else if (currentPhase == Phase.ATTACKING)
        {
            AttackPhase();
        }
    }

    void ResolveAttack(CombatController target)
    {
        CharacterSheet targetSheet = target.characterSheet;
        AttackEffects(targetSheet);
    }

    virtual protected void AttackEffects(CharacterSheet targetStats)
    {
        characterSheet.PerformBasicAttack(targetStats);
    }

    void AttackPhase()
    {
        if (path.Count == 1)
        {
            Tile targetTile = path.Pop();
            Vector3 direction = CalculateDirection(targetTile.transform.position);
            if (direction != Vector3.zero)
                transform.up = new Vector3(direction.x, direction.y, 0f);
            ResolveAttack(targetTile.occupant);
        }
        else
        {
            StartCoroutine(EndActionAfterDelay(ATTACK_DURATION));
        }
    }

}
