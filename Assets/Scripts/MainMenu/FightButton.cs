using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    public void LoadCombat()
    {
        EnemyParty.Reset();
        SceneManager.LoadScene(Constants.SCENE_COMBAT);
    }
}
