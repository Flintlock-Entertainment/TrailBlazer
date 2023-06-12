using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject
{
    [SerializeField] public Faction Faction;
    [SerializeField] public BaseUnit UnitPrefab;

    [SerializeField] private int actionsPerTurn;

    [SerializeField] private int baseHP;

    [SerializeField] private int classHP;

    [SerializeField] private int currentHP;

    [SerializeField] private int[] Stats;

    [SerializeField] private int Speed;

    [SerializeField] private int Coins;

    [SerializeField] private int Level;

    [SerializeField] private List<string> Languages;

    [SerializeField] private Proficiency[] SaveProf;

    [SerializeField] private Proficiency WeaponProf;

    private SkillManager skillManager;

    [SerializeField] public ScriptableWeapon MainHand;

    [SerializeField] public ScriptableItem OffHand;

    [SerializeField] private ScriptableWearableItem Head;

    [SerializeField] private ScriptableWearableItem Body;

    [SerializeField] private ScriptableWearableItem Legs;

    public ScriptableUnit()
    {
        Languages = new List<string>();
        Stats = new int[6] { 0, 0, 0, 0, 0, 0 };
        SaveProf = new Proficiency[3] { Proficiency.Untrained, Proficiency.Untrained, Proficiency.Untrained };
        skillManager = new SkillManager();
        currentHP = 0;
    }

    private void Awake()
    {
        currentHP = 0;
    }
    public virtual int GetReflexSave()
    {
        return GetSave(Saves.Reflex) + GetStat(Abilities.Dexterity);
    }

    public virtual int GetFortitueSave()
    {
        return GetSave(Saves.Fortitude) + GetStat(Abilities.Constitution);
    }

    public virtual int GetWillSave()
    {
        return GetSave(Saves.Will) + GetStat(Abilities.Wisdom);
    }

    public virtual int GetStat(Abilities ability)
    {
        return Stats[Convert.ToInt32(ability)];
    }

    public virtual void SetStat(Abilities ability, int stat)
    {
         Stats[Convert.ToInt32(ability)] = stat;
    }

    public virtual int GetActionsPerTurn()
    {
        return actionsPerTurn;
    }

    public virtual int GetHP()
    {
        return baseHP + (GetStat(Abilities.Constitution) + classHP) * Level;
    }

    public virtual int GetCurrentHP()
    {
        return currentHP;
    }
    public virtual void SetCurrentHP(int value)
    {
        currentHP = value;
    }

    public virtual void UpdateCurrentHP(int update)
    {
        currentHP += update;
    }
    public virtual int GetAC()
    {
        return Head.GetAC(GetStat(Abilities.Dexterity)) + Body.GetAC(GetStat(Abilities.Dexterity)) + Legs.GetAC(GetStat(Abilities.Dexterity));
    }

    public virtual int GetSpeed()
    {
        return Speed;
    }

    public virtual int GetCoins()
    {
        return Coins;
    }

    public virtual void UpdateCoins(int update)
    {
        Coins += update;
    }

    public virtual int GetLevel()
    {
        return Level;
    }

    public virtual int GetWeaponProf()
    {
        return Convert.ToInt32(WeaponProf);
    }

    public virtual int GetSkill(Skills skill)
    {
        return skillManager.GetSkillProf(skill) + GetStat(skillManager.GetSkillAbility(skill));
    }

    protected virtual int GetSave(Saves save)
    {
        return Convert.ToInt32(SaveProf[Convert.ToInt32(save)]);
    }

    public virtual ScriptableWeapon GetMainHand()
    {
        return MainHand;
    }

    public virtual ScriptableItem GetOffHand()
    {
        return OffHand;
    }

    public virtual string GetDescription()
    {
        string desc =
            "str= " + "?" + "  " + "HP= " + "?" + "/" + "?" + "\n" +
            "dex= " + "?" + "  " + "speed= " + "?" + "\n" +
            "con= " + "?" + "  " + "AC= " + "?" + "\n" +
            "int= " + "?" + "  " + "Reflex= " + "?" + "\n" +
            "wis= " + "?" + "  " + "Fortitude= " + "?" + "\n" +
            "cha= " + "?" + "  " + "Will= " + "?" + "\n";

        return desc;
    }
    protected class SkillManager
    {
        public Proficiency[] SkillProf;
        public Dictionary<Skills, Abilities> skillsAbilityMap;

        public SkillManager()
        {
            SkillProf = new Proficiency[14];
            skillsAbilityMap = new Dictionary<Skills, Abilities>();
            int[] abilityMap = new int[14] { 1, 3, 0, 3, 5, 5, 5, 4, 4, 5, 4, 1, 4, 1 };
            for (int i = 0; i < SkillProf.Length; i++)
            {
                SkillProf[i] = Proficiency.Untrained;
                skillsAbilityMap[(Skills)i] = (Abilities)abilityMap[i];
            }


        }

        public Abilities GetSkillAbility(Skills skill)
        {
            return skillsAbilityMap[skill];
        }

        public int GetSkillProf(Skills skill)
        {
            return Convert.ToInt32(SkillProf[Convert.ToInt32(skill)]);
        }


    }
}



public enum Faction
{
    Character = 0,
    Enemy = 1
}

public enum Proficiency
{
    Untrained = 0,
    Trained = 2,
    Expert = 4,
    Master = 6,
    Legendary = 8
}

public enum Abilities
{
    Strength = 0,
    Dexterity = 1,
    Constitution = 2,
    Intelligence = 3,
    Wisdom = 4,
    Charisma = 5
}

public enum Saves
{
    Fortitude = 0,
    Reflex = 1,
    Will = 2
}

public enum Skills
{
    Acrobatics = 0,
    Arcana = 1,
    Athletics = 2,
    Crafting = 3,
    Deception = 4,
    Diplomacy = 5,
    Intimidation = 6,
    Medicine = 7,
    Nature = 8,
    Performance = 9,
    Religion = 10,
    Stealth = 11,
    Survival = 12,
    Thievery = 13,
}