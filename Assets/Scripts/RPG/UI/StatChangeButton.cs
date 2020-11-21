using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatChangeButton : MonoBehaviour
{
    public enum BoostType
    {
        BOOST_TYPE_MIGHT,
        BOOST_TYPE_FINESSE,
        BOOST_TYPE_INTELLECT
    };

    [SerializeField] public BoostType boostType;
    [SerializeField] public bool upOrDown;
    public CharacterSheet character;

    public void Start()
    {
        character = CharsheetUIInitializer.characterBeingDisplayed;
    }

    public void ClickBoostButton()
    {
        switch (boostType)
        {
            case BoostType.BOOST_TYPE_MIGHT:
                if (upOrDown) character.BoostMight(); else character.DeBoostMight();
                break;
            case BoostType.BOOST_TYPE_FINESSE:
                if (upOrDown) character.BoostFinesse(); else character.DeBoostFinesse();
                break;
            case BoostType.BOOST_TYPE_INTELLECT:
                if (upOrDown) character.BoostIntellect(); else character.DeBoostIntellect();
                break;
        }
        GameObject.FindObjectOfType<CharSheetUIManager>().Refresh();
    }

}
