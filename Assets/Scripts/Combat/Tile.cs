using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    [SerializeField] public List<Tile> neighbors = new List<Tile>();
    [SerializeField] public int x, y;

    public CombatController occupant = null;
    public bool isWalkable = true;

    public bool requiresRun = false;

    public bool isHovered = false;

    // Needed for breadth-first search
    public bool wasVisited = false;
    public bool canBeChosen = false;
    public Tile parent = null;
    public int distance = 0;

    // Use this for initialization
    void Start()
    {
        TurnManager m = GameObject.FindObjectOfType<TurnManager>();
        if (x > 0) neighbors.Add(m.tileGrid[x - 1, y]);
        if (x < Constants.COMBAT_WIDTH - 1) neighbors.Add(m.tileGrid[x + 1, y]);
        if (y > 0) neighbors.Add(m.tileGrid[x, y - 1]);
        if (y < Constants.COMBAT_HEIGHT - 1) neighbors.Add(m.tileGrid[x, y + 1]);
    }

    public int GetMoveCost()
    {
        return 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovered) GetComponent<Renderer>().material.color = Color.magenta;
        else if (canBeChosen)
        {
            if (occupant != null)
                GetComponent<Renderer>().material.color = Color.red;
            else if (requiresRun)
                GetComponent<Renderer>().material.color = Color.yellow;
            else
                GetComponent<Renderer>().material.color = Color.green;
        }
        else GetComponent<Renderer>().material.color = Color.white;
    }

    public void ResetSearch()
    {
        isHovered = false;
        canBeChosen = false;
        requiresRun = false;
        wasVisited = false;
        parent = null;
        distance = 0;
    }

}
