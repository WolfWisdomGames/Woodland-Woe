using UnityEngine;
using System.Collections.Generic;

public class Tile : MonoBehaviour
{
    [SerializeField] public List<Tile> neighbors = new List<Tile>();
    [SerializeField] public int x, y;

    public bool isWalkable = true;

    // Needed for breadth-first search
    public bool wasVisited = false;
    public Tile parent = null;
    public int distance = 0;

    // Use this for initialization
    void Start()
    {
        if (x > 0) neighbors.Add(CombatManager.tileGrid[x - 1, y]);
        if (x < Constants.COMBAT_WIDTH - 1) neighbors.Add(CombatManager.tileGrid[x + 1, y]);
        if (y > 0) neighbors.Add(CombatManager.tileGrid[x, y - 1]);
        if (y < Constants.COMBAT_HEIGHT - 1) neighbors.Add(CombatManager.tileGrid[x, y + 1]);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
