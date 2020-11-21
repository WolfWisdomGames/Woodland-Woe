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

    [SerializeField]
    GameObject HealthValueText;

    [SerializeField]
    GameObject DamageValueText;

    [SerializeField]
    GameObject MoveValueText;

    [SerializeField]
    GameObject AccuracyValueText;

    [SerializeField]
    GameObject DodgeValueText;

    [SerializeField]
    GameObject BackstabValueText;

    [SerializeField]
    GameObject ManaRegainValueText;

    [SerializeField]
    GameObject CritChanceValueText;

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
        HealthValueText.GetComponent<Text>().text = characterBeingDisplayed.currentHealth.ToString() + "/" + characterBeingDisplayed.MaxHealth().ToString();
        DamageValueText.GetComponent<Text>().text = characterBeingDisplayed.MinDamage().ToString() + "-" + characterBeingDisplayed.MaxDamage().ToString();
        MoveValueText.GetComponent<Text>().text = characterBeingDisplayed.MoveSpeed().ToString();
        AccuracyValueText.GetComponent<Text>().text = characterBeingDisplayed.HitPercent().ToString();
        DodgeValueText.GetComponent<Text>().text = characterBeingDisplayed.DodgePercent().ToString();
        BackstabValueText.GetComponent<Text>().text = characterBeingDisplayed.BackStab().ToString();
        ManaRegainValueText.GetComponent<Text>().text = characterBeingDisplayed.ManaRegain().ToString();
        CritChanceValueText.GetComponent<Text>().text = characterBeingDisplayed.CritPercent().ToString();
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
