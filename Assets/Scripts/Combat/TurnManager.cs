using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour
{
    public Tile[,] tileGrid;
    public List<CombatController> combatants = new List<CombatController>();

    private int moveIdx = -1;
    private bool enemyTurn = false;
    private bool frozen = false;
    private bool gameOver = false;

    CombatController GetCurrentCombatController()
    {
        if (moveIdx == -1) return null;
        if (combatants[moveIdx] == null)
            return null;
        else if (!combatants[moveIdx].Dead())
        {
            return combatants[moveIdx];
        }
        return null;
    }

    public void ResetTileSearch()
    {
        for (int x = 0; x < tileGrid.GetLength(0); x++)
            for (int y = 0; y < tileGrid.GetLength(1); y++)
                tileGrid[x, y].ResetSearch();
    }

    void BeginTurn()
    {
        CombatController controller = GetCurrentCombatController();
        controller.BeginTurn();
        // DisplayCurrentCreatureStats();
    }

    private IEnumerator BeginTurnAfterDelay(float fDuration)
    {
        frozen = true;
        float elapsed = 0f;
        while (elapsed < fDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }
        frozen = false;
        BeginTurn();
        yield break;
    }

    void AdvanceToNextTurn()
    {
        moveIdx = (moveIdx + 1) % combatants.Count;
        if (GetCurrentCombatController() != null)
        {
            StartCoroutine(BeginTurnAfterDelay(0.25f));
        }
    }

    void Update()
    {
        if (frozen || gameOver) return;
        if (GetCurrentCombatController() == null || !GetCurrentCombatController().isTurn)
        {
            AdvanceToNextTurn();
        }
    }
}
