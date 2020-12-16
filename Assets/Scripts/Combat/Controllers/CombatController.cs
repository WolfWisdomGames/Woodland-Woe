using UnityEngine;
using System.Collections;

public class CombatController : MonoBehaviour
{

    protected CharacterSheet characterSheet = null;

    // Use this for initialization
    void Start()
    {
    }

    public void SetCharacterSheet(CharacterSheet c)
    {
        characterSheet = c;
    }

}
