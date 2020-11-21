using UnityEngine;
using System.Collections;

public class CharacterSheet
{
    public int currentHealth;

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

    // Use this for initialization
    public CharacterSheet(string newName, int newLevel)
    {
        might = 1;
        finesse = 1;
        intellect = 1;
        xp = (newLevel - 1) * 1000;
        level = newLevel;
        currentHealth = MaxHealth();
        freeStatPoints = (newLevel - 1) * 2;
        name = newName;
        ConfirmChanges();
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
        return intellect * 2;
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
