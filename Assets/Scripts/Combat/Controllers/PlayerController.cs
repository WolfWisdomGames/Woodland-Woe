﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerController : CombatController
{

    private Tile hoverTile = null;

    void Update()
    {
        if (!isTurn)
        {
            return;
        }
        if (!isActing)
        {
            CheckMouseClick();
        }
    }

    override public bool IsPC()
    {
        return true;
    }

    override protected bool ContainsEnemy(Tile tile)
    {
        if (tile.occupant == null) return false;
        return tile.occupant.IsEnemy();
    }

    private Tile GetMouseTile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<Tile>() != null)
        {
            Tile mouseTile = hit.collider.GetComponent<Tile>();
            return mouseTile;
        }
        return null;
    }

    void ClearMouseHover()
    {
        if (hoverTile != null) hoverTile.isHovered = false;
        foreach (GameObject line in GameObject.FindGameObjectsWithTag("LineTag"))
        {
            Destroy(line);
        }
    }

    void LineBetweenPositions(Vector3 start, Vector3 end)
    {
        GameObject lineObject = new GameObject("Line");
        lineObject.tag = "LineTag";
        LineRenderer line = lineObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        line.positionCount = 2;
        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        Vector3[] points = new Vector3[2];
        points[0] = start;
        points[1] = end;
        line.SetPositions(points);
    }

    private void SetMouseHover()
    {
        hoverTile = GetMouseTile();
        if (hoverTile == null) return;
        hoverTile.isHovered = true;
        Tile t = hoverTile;
        while (t.parent)
        {
            LineBetweenPositions(t.transform.position, t.parent.transform.position);
            t = t.parent;
        }
    }

    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Tile clickedTile = GetMouseTile();
            if (clickedTile == null || !clickedTile.canBeChosen) return;
            ClearMouseHover();
            if (clickedTile.occupant != null)
            {
                selectedAction.BeginAction(clickedTile);
                selectedAction = GetComponent<ActionBasicAttack>();
                return;
            }
            else
            {
                Action move = GetComponent<ActionMove>();
                move.BeginAction(clickedTile);
                return;
            }
        }
        else
        {
            if (GetMouseTile() != hoverTile)
            {
                ClearMouseHover();
                SetMouseHover();
            }
        }
    }
}
