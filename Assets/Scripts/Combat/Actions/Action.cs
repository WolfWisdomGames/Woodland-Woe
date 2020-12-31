using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a base class that all other types of actions inherit from.
public class Action : MonoBehaviour
{
    protected enum Phase
    {
        NONE,
        MOVING,
        ATTACKING,
    };

    public enum TargetType
    {
        NONE,
        MELEE,
    };

    public enum ActionType
    {
        FREE,
        MOVE_EQUIVILANT,
        ATTACK,
        FULL_ROUND
    };

    protected Animator anim;
    protected bool inProgress = false;
    protected CombatController combatController;
    protected CharacterSheet characterSheet;

    virtual public int MANA_COST { get { return 0; } }

    // Target type for special moves, lets UI/AI know when it can use special moves.
    virtual public TargetType TARGET_TYPE { get { return TargetType.NONE; } }

    virtual public ActionType ACTION_TYPE { get { return ActionType.ATTACK; } }

    protected Phase currentPhase = Phase.NONE;

    protected IEnumerator EndActionAfterDelay(float fDuration)
    {
        yield return new WaitForSeconds(fDuration);
        currentPhase = Phase.NONE;
        EndAction();
        yield break;
    }

    virtual public string DisplayName()
    {
        return "";
    }

    // Start is called before the first frame update
    virtual protected void Start()
    {
        combatController = GetComponent<CombatController>();
        anim = GetComponentInChildren<Animator>();
        characterSheet = combatController.characterSheet;
    }

    virtual public void BeginAction(Tile targetTile)
    {
        inProgress = true;
        characterSheet.DisplayPopupDuringCombat(DisplayName());
        combatController.BeginAction();
    }

    protected void EndAction()
    {
        characterSheet.currentMana -= MANA_COST;
        combatController.EndAction(ACTION_TYPE);
        inProgress = false;
    }
}
