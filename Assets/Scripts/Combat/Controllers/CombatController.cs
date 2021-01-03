using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatController : MonoBehaviour
{
    public CharacterSheet characterSheet = null;
    public bool isTurn = false;
    public bool isActing = false;
    protected Action selectedAction = null;
    protected List<Tile> selectableTiles = new List<Tile>();
    protected TurnManager manager;

    protected bool hasMoved = false;
    private Tile currentTile;

    // Use this for initialization
    void Start()
    {
        manager = FindObjectOfType<TurnManager>();
        selectedAction = GetComponent<ActionBasicAttack>();
    }

    virtual public bool IsPC()
    {
        return false;
    }

    virtual public bool IsEnemy()
    {
        return false;
    }

    // Checks to see if the input tile contains an enemy of this.
    // Defaults to false, but can be overridden by subclasses.
    // Note that 'enemy' is from the perspective of the actor;
    // for player-controlled, enemies are AI and vice versa.
    virtual protected bool ContainsEnemy(Tile tile)
    {
        return false;
    }

    public void SetCharacterSheet(CharacterSheet c)
    {
        characterSheet = c;
    }

    public bool Dead()
    {
        return false;
    }

    public void BeginTurn()
    {
        if (Dead()) return;
        if (DoesGUI())
        {
            // characterSheet.UpdateUI();
        }
        isTurn = true;
        hasMoved = false;
        FindSelectableBasicTiles();
    }

    // Defaults to false, but can be overridden by subclasses.
    // If true, the unit is interactable via the GUI.
    virtual protected bool DoesGUI()
    {
        return false;
    }

    protected void EndTurn()
    {
        isTurn = false;
    }

    public void BeginAction()
    {
        isActing = true;
    }

    public void EndAction(Action.ActionType t)
    {
        isActing = false;
        manager.ResetTileSearch();
        if (t == Action.ActionType.MOVE_EQUIVILANT && !hasMoved)
        {
            hasMoved = true;
            FindSelectableBasicTiles();
        }
        else
        {
            EndTurn();
        }
    }

    public void SetCurrentTile(Tile t)
    {
        // Unoccupy old tile, if any
        if (currentTile != null)
        {
            currentTile.occupant = null;
        }
        t.occupant = this;
        currentTile = t;
    }

    virtual protected bool HasEnemy(Tile t)
    {
        return false;
    }

    protected void FindSelectableBasicTiles()
    {
        FindSelectableTiles();
    }

    private void FindSelectableTiles()
    {
        manager.ResetTileSearch();
        selectableTiles.Clear();

        // TODO: Replace with PriorityQueue for performance optimization
        List<Tile> queue = new List<Tile>();
        queue.Add(currentTile);
        currentTile.wasVisited = true;

        while (queue.Count > 0)
        {
            queue.Sort((item1, item2) => item1.distance.CompareTo(item2.distance));
            Tile tile = queue[0];
            queue.RemoveAt(0);

            if (tile != currentTile)
            {
                selectableTiles.Add(tile);
                tile.canBeChosen = true;
                tile.wasVisited = true;
                if (tile.distance > characterSheet.MoveSpeed() || hasMoved) tile.requiresRun = true;
            }

            foreach (Tile adjacentTile in tile.neighbors)
            {
                if (!adjacentTile.wasVisited)
                {
                    if (tile.distance <= characterSheet.MoveSpeed() * (hasMoved ? 0 : 1) && ContainsEnemy(adjacentTile))
                    {
                        AttachTile(adjacentTile, tile);
                        selectableTiles.Add(adjacentTile);
                        adjacentTile.canBeChosen = true;
                        adjacentTile.wasVisited = true;
                    }
                    // Potential children in search tree.
                    if (adjacentTile.occupant == null && !adjacentTile.wasVisited)
                        if (adjacentTile.GetMoveCost() + tile.distance <= characterSheet.MoveSpeed() * (hasMoved ? 1 : 2))
                        {
                            AttachTile(adjacentTile, tile);
                            queue.Add(adjacentTile);
                        }
                }
            }
        }
    }

    private void AttachTile(Tile adjacentTile, Tile parent)
    {
        adjacentTile.parent = parent;
        adjacentTile.distance = adjacentTile.GetMoveCost() + parent.distance;
    }
}
