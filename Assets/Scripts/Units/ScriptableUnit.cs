using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    public Faction Faction;
    public BaseUnit UnitPrefab;

    public int HP;

    public int AC;

    public int[] Stats;

    public int Speed;

    public int Level;

    public List<string> Languages;

    public Proficiency[] SaveProf;

    public Proficiency WeaponProf;

    private SkillManager skillManager;

    public ScriptableItem MainHand;

    public ScriptableItem OffHand;

    public ScriptableWearableItem Head;

    public ScriptableWearableItem Body;

    public ScriptableWearableItem Legs;

    public ScriptableUnit()
    {
        Languages = new List<string>();
        Stats = new int[6] { 0, 0, 0, 0, 0, 0 };
        SaveProf = new Proficiency[3] { Proficiency.Untrained, Proficiency.Untrained, Proficiency.Untrained };
        skillManager = new SkillManager();
    }

    public int GetReflexSave()
    {
        return GetSave(Saves.Reflex) + GetStat(Abilities.Dexterity);
    }

    public int GetFortitueSave()
    {
        return GetSave(Saves.Fortitude) + GetStat(Abilities.Constitution);
    }

    public int GetWillSave()
    {
        return GetSave(Saves.Will) + GetStat(Abilities.Wisdom);
    }

    public int GetStat(Abilities ability)
    {
        return Stats[Convert.ToInt32(ability)];
    }

    public int GetAC()
    {
        return Head.GetAC(GetStat(Abilities.Dexterity)) + Body.GetAC(GetStat(Abilities.Dexterity)) + Legs.GetAC(GetStat(Abilities.Dexterity));
    }

    public int GetWeaponProf()
    {
        return Convert.ToInt32(WeaponProf);
    }

    public int GetSkill(Skills skill)
    {
        return skillManager.GetSkillProf(skill) + GetStat(skillManager.GetSkillAbility(skill));
    }

    private int GetSave(Saves save)
    {
        return Convert.ToInt32(SaveProf[Convert.ToInt32(save)]);
    }

    private class SkillManager
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
                skillsAbilityMap[(Skills) i] = (Abilities) abilityMap[i];
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



public enum Faction {
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