﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMove : Action
{
    [SerializeField] private float moveSpeed = 2;

    protected Stack<Tile> path = new Stack<Tile>();

    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
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
            currentPhase = Phase.NONE;
            EndAction();
        }
    }

    protected void Move()
    {
        if (path.Count > 0)
        {
            Tile tile = path.Peek();
            Vector3 targetPos = tile.transform.position;

            if (Vector3.Distance(transform.position, targetPos) >= 0.01f)
            {
                Vector3 direction = CalculateDirection(targetPos);
                transform.forward = new Vector3(direction.x, direction.y, 180.0f);
                direction = CalculateDirection(targetPos);
                Vector3 velocity = GetHorizontalVelocity(direction);
                transform.Translate(velocity * Time.deltaTime);
            }
            else
            {
                // Center of tile reached
                transform.position = targetPos;
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

    private Vector3 GetHorizontalVelocity(Vector3 direction)
    {
        return direction * moveSpeed;
    }
}