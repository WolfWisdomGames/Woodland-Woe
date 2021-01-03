using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMove : Action
{
    [SerializeField] private float moveSpeed = 2;
    override public ActionType ACTION_TYPE { get { return (hasRun ? ActionType.FULL_ROUND : ActionType.MOVE_EQUIVILANT); } }
    protected Stack<Tile> path = new Stack<Tile>();
    private bool hasRun = false;
    protected int reserveTiles = 0;

    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!inProgress)
        {
            return;
        }
        if (currentPhase == Phase.MOVING)
        {
            Move();
        }
        else
        {
            EndAction();
        }
    }

    protected void Move()
    {
        if (path.Count > reserveTiles)
        {
            Tile tile = path.Peek();
            Vector3 targetPos = tile.transform.position;

            if (Vector3.Distance(transform.position, targetPos) >= 0.01f)
            {
                Vector3 direction = CalculateDirection(targetPos);
                transform.up = new Vector3(direction.x, direction.y, 0f);
                transform.Translate(direction * Time.deltaTime * moveSpeed, Space.World);
            }
            else
            {
                // Center of tile reached
                transform.position = targetPos;
                if (path.Count == 1 + reserveTiles)
                    combatController.SetCurrentTile(path.Peek());
                if (path.Peek().requiresRun) hasRun = true;
                path.Pop();
            }
        }
        else
        {
            // Done moving.
            currentPhase = Phase.ATTACKING;
        }
    }

    override public void BeginAction(Tile targetTile)
    {
        hasRun = false;
        currentPhase = Phase.MOVING;
        PreparePath(targetTile);
        base.BeginAction(targetTile);
    }


    private void PreparePath(Tile targetTile)
    {
        path.Clear();

        Tile next = targetTile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }

    protected Vector3 CalculateDirection(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        return direction.normalized;
    }
}