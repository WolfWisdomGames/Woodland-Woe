using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSheetUIManager : MonoBehaviour
{

    [SerializeField]
    GameObject NameText;

    [SerializeField]
    GameObject MigValueText;

    [SerializeField]
    GameObject FinValueText;

    [SerializeField]
    GameObject IntValueText;

    [SerializeField]
    GameObject LevelValueText;

    [SerializeField]
    GameObject XPValueText;

    [SerializeField]
    GameObject BonusPointsValueText;

    [SerializeField]
    GameObject BoostMightButton;

    [SerializeField]
    GameObject BoostFinesseButton;

    [SerializeField]
    GameObject BoostIntellectButton;

    [SerializeField]
    GameObject DeBoostMightButton;

    [SerializeField]
    GameObject DeBoostFinesseButton;

    [SerializeField]
    GameObject DeBoostIntellectButton;

    private CharacterSheet characterBeingDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        characterBeingDisplayed = CharsheetUIInitializer.characterBeingDisplayed;
        Refresh();
    }

    public void Refresh()
    {
        MigValueText.GetComponent<Text>().text = characterBeingDisplayed.might.ToString();
        FinValueText.GetComponent<Text>().text = characterBeingDisplayed.finesse.ToString();
        IntValueText.GetComponent<Text>().text = characterBeingDisplayed.intellect.ToString();
        NameText.GetComponent<Text>().text = characterBeingDisplayed.name.ToString();
        LevelValueText.GetComponent<Text>().text = characterBeingDisplayed.level.ToString();
        BonusPointsValueText.GetComponent<Text>().text = characterBeingDisplayed.freeStatPoints.ToString();
        XPValueText.GetComponent<Text>().text = characterBeingDisplayed.xp.ToString() + "/" + characterBeingDisplayed.NextLevelXp().ToString();
        if (characterBeingDisplayed.CanBoostMight())
            BoostMightButton.SetActive(true);
        else
            BoostMightButton.SetActive(false);
        if (characterBeingDisplayed.CanBoostFinesse())
            BoostFinesseButton.SetActive(true);
        else
            BoostFinesseButton.SetActive(false);
        if (characterBeingDisplayed.CanBoostIntellect())
            BoostIntellectButton.SetActive(true);
        else
            BoostIntellectButton.SetActive(false);
        if (characterBeingDisplayed.CanDeBoostMight())
            DeBoostMightButton.SetActive(true);
        else
            DeBoostMightButton.SetActive(false);
        if (characterBeingDisplayed.CanDeBoostFinesse())
            DeBoostFinesseButton.SetActive(true);
        else
            DeBoostFinesseButton.SetActive(false);
        if (characterBeingDisplayed.CanDeBoostIntellect())
            DeBoostIntellectButton.SetActive(true);
        else
            DeBoostIntellectButton.SetActive(false);
    }
}
