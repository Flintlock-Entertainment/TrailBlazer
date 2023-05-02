using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    public Faction Faction;
    public BaseUnit UnitPrefab;

    public int HP;

    public int AC;

    public int[] Stats = new int[6];

    public int Speed;

    public int Level;

    public List<string> Languages;

    public Proficiency[] SaveProf = new Proficiency[3];

    public Proficiency[] SkillProf = new Proficiency[15];

    public Item MainHand;

    public Item OffHand;

    public WearableItem Head;

    public WearableItem Body;

    public WearableItem Legs;

    public ScriptableUnit()
    {
        Languages = new List<string>();
        Stats = new int[6] { 0, 0, 0, 0, 0, 0 };
        SaveProf = new Proficiency[3] { Proficiency.Untrained, Proficiency.Untrained, Proficiency.Untrained };
        SkillProf = new Proficiency[15];
        for(int i = 0; i < SkillProf.Length; i++)
        {
            SkillProf[i] = Proficiency.Untrained;
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