using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterSheet
{
    public int currentHealth;

    public int currentMana;

    public int level;
    public int xp;

    public int freeStatPoints;

    public string name;

    public int might;
    public int finesse;
    public int intellect;

    private int minMight;
    private int minFinesse;
    private int minIntellect;

    private string avatarFileName;

    public GameObject avatar;
    private IndicatorBar healthBar;
    private IndicatorBar manaBar;
    private CombatController combatController;
    public bool dead;

    // Use this for initialization
    public CharacterSheet(string newName, int newLevel, string newAvatarFileName)
    {
        might = 1;
        finesse = 1;
        intellect = 1;
        xp = (newLevel - 1) * 1000;
        level = newLevel;
        currentHealth = MaxHealth();
        freeStatPoints = (newLevel - 1) * 2;
        name = newName;
        avatarFileName = newAvatarFileName;
        ConfirmChanges();
    }

    public CombatController SpawnCombatAvatar(Vector3 location, Quaternion rotation, bool asPC)
    {
        dead = false;
        if (currentHealth <= 0) currentHealth = 1;
        GameObject combatPrefab = (GameObject)Resources.Load("Prefabs/Combat/" + avatarFileName, typeof(GameObject));
        avatar = GameObject.Instantiate(combatPrefab, location, rotation) as GameObject;
        healthBar = avatar.transform.Find("Canvas").transform.Find("HealthBar").GetComponent<IndicatorBar>();
        healthBar.SetSliderMax(MaxHealth());
        healthBar.SetSlider(currentHealth);
        manaBar = avatar.transform.Find("Canvas").transform.Find("ManaBar").GetComponent<IndicatorBar>();
        manaBar.SetSliderMax(MaxMana());
        manaBar.SetSlider(currentMana);
        avatar.AddComponent<ActionMove>();
        avatar.AddComponent<ActionBasicAttack>();
        if (asPC)
        {
            combatController = avatar.AddComponent<PlayerController>();
        }
        else
        {
            combatController = avatar.AddComponent<EnemyController>();
        }
        combatController.SetCharacterSheet(this);
        GameObject.FindObjectOfType<TurnManager>().combatants.Add(combatController);
        // List<Action> specialMoves = new List<Action> { };
        return combatController;
    }

    public void ReceiveDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            dead = true;
            combatController.Die();
            GameObject.Destroy(avatar);
        }
        healthBar.SetSlider(currentHealth);
    }

    public void PerformBasicAttack(CharacterSheet target)
    {
        int dam = MinDamage() + Random.Range(0, 1 + MaxDamage() - MinDamage());
        target.ReceiveDamage(dam);
    }

    // This is intended to only be used during combat.
    public void DisplayPopupDuringCombat(string popupText)
    {

    }

    public bool CanDeploy()
    {
        return true;
    }

    public int MinDamage()
    {
        return 2 + might * 2;
    }

    public int MaxDamage()
    {
        return 2 + might * 2 + level;
    }

    public int MaxHealth()
    {
        return (5 * level) + (2 * might);
    }

    public int MaxMana()
    {
        return 10 * intellect;
    }

    public int MoveSpeed()
    {
        return 4 + finesse / 3;
    }

    public int BackStab()
    {
        return 2 + finesse;
    }

    public int HitPercent()
    {
        return 75 + 5 * finesse;
    }

    public int DodgePercent()
    {
        return 5 * finesse;
    }

    public int ManaRegain()
    {
        return intellect;
    }

    public int CritPercent()
    {
        return finesse * 3;
    }

    public int NextLevelXp()
    {
        return level * ((level + 1) * 500);
    }

    public void ConfirmChanges()
    {
        minMight = might;
        minFinesse = finesse;
        minIntellect = intellect;
    }

    public void BoostMight()
    {
        freeStatPoints--;
        might++;
    }

    public void BoostFinesse()
    {
        freeStatPoints--;
        finesse++;
    }

    public void BoostIntellect()
    {
        freeStatPoints--;
        intellect++;
    }

    public void DeBoostMight()
    {
        freeStatPoints++;
        might--;
    }

    public void DeBoostFinesse()
    {
        freeStatPoints++;
        finesse--;
    }

    public void DeBoostIntellect()
    {
        freeStatPoints++;
        intellect--;
    }

    public bool CanBoostMight()
    {
        return freeStatPoints > 0 && might < 15;
    }

    public bool CanBoostFinesse()
    {
        return freeStatPoints > 0 && finesse < 15;
    }

    public bool CanBoostIntellect()
    {
        return freeStatPoints > 0 && intellect < 15;
    }

    public bool CanDeBoostMight()
    {
        return might > minMight;
    }

    public bool CanDeBoostFinesse()
    {
        return finesse > minFinesse;
    }

    public bool CanDeBoostIntellect()
    {
        return intellect > minIntellect;
    }

}
