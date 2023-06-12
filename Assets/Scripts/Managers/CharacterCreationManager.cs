using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class CharacterCreationManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NumStr, NumDex, NumCon, NumInt, NumWis, NumCha;
    private int numStr, numDex, numCon, numInt, numWis, numCha;

    [SerializeField]
    private TextMeshProUGUI Total;
    private int total;

    private int Loadout;

    [SerializeField]
    private GameObject[] circles; 
    // Start is called before the first frame update
    void Start()
    {
        total = 12;
        numStr = 0;
        numDex = 0;
        numCon = 0;
        numInt = 0;
        numWis = 0;
        numCha = 0;
        Loadout = 4;
    }

    public void AddStr()
    {
        AddToStat(Abilities.Strength);
    }
    public void AddDex()
    {
        AddToStat(Abilities.Dexterity);
    }
    public void AddCon()
    {
        AddToStat(Abilities.Constitution);
    }
    public void AddInt()
    {
        AddToStat(Abilities.Intelligence);
    }
    public void AddWis()
    {
        AddToStat(Abilities.Wisdom);
    }
    public void AddCha()
    {
        AddToStat(Abilities.Charisma);
    }

    public void SubStr()
    {
        SubFromStat(Abilities.Strength);
    }
    public void SubDex()
    {
        SubFromStat(Abilities.Dexterity);
    }
    public void SubCon()
    {
        SubFromStat(Abilities.Constitution);
    }
    public void SubInt()
    {
        SubFromStat(Abilities.Intelligence);
    }
    public void SubWis()
    {
        SubFromStat(Abilities.Wisdom);
    }
    public void SubCha()
    {
        SubFromStat(Abilities.Charisma);
    }

    public void Continue()
    {
        if (total != 0 || Loadout == 4)
            return;
        Debug.Log("Continue");
        var _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        var character = _units.Where(u => u.Faction == Faction.Character).First();
        
        character.SetStat(Abilities.Strength, numStr);
        character.SetStat(Abilities.Dexterity, numDex);
        character.SetStat(Abilities.Constitution, numCon);
        character.SetStat(Abilities.Intelligence, numInt);
        character.SetStat(Abilities.Wisdom, numWis);
        character.SetStat(Abilities.Charisma, numCha);

        var _items = Resources.LoadAll<ScriptableWeapon>("Items").ToList().Where(i => i.layer == Layer.LastLayer);
        switch (Loadout)
        {
            case (0):
                character.MainHand = _items.Where(i => i.GetItemName() == "LongSword").First();
                character.OffHand = Resources.LoadAll<ScriptableWearableItem>("Items").ToList().Where(i => i.layer == Layer.LastLayer && i.GetItemName() == "Shield").First();
                break;
            case (1):
                character.MainHand = _items.Where(i => i.GetItemName() == "GiantSword").First();
                character.OffHand = _items.Where(i => i.GetItemName() == "NullWeapon").First();
                break;
            case (2):
                character.MainHand = _items.Where(i => i.GetItemName() == "ShortSword").First();
                character.OffHand = _items.Where(i => i.GetItemName() == "ShortSword").First();
                break;
            case (3):
                character.MainHand = _items.Where(i => i.GetItemName() == "Bow").First();
                character.OffHand = _items.Where(i => i.GetItemName() == "Dagger").First();
                break;
        }
        character.SetCurrentHP(character.GetHP());
        PlayerPrefs.DeleteKey("Map");
        SceneManager.LoadScene("MainMap");
    }

    public void Defender()
    {
        Loadout = 0;
        ActivateCircle(Loadout);
    }
    public void Champion()
    {
        Loadout = 1;
        ActivateCircle(Loadout);
    }
    public void Flurry()
    {
        Loadout = 2;
        ActivateCircle(Loadout);
    }
    public void Ranger()
    {
        Loadout = 3;
        ActivateCircle(Loadout);
    }

    private void AddToStat(Abilities abilitiy)
    {
        if (total == 0)
            return;
        switch (abilitiy)
        {
            case (Abilities.Strength):
                if(numStr < 3)
                {
                    numStr++;
                    NumStr.text = numStr.ToString();
                    UpdateTotal(-1);
                }
                break;
            case (Abilities.Dexterity):
                if (numDex < 3)
                {
                    numDex++;
                    NumDex.text = numDex.ToString();
                    UpdateTotal(-1);
                }
                break;
            case (Abilities.Constitution):
                if (numCon < 3)
                {
                    numCon++;
                    NumCon.text = numCon.ToString();
                    UpdateTotal(-1);
                }
                break;
            case (Abilities.Intelligence):
                if (numInt < 3)
                {
                    numInt++;
                    NumInt.text = numInt.ToString();
                    UpdateTotal(-1);
                }
                break;
            case (Abilities.Wisdom):
                if (numWis < 3)
                {
                    numWis++;
                    NumWis.text = numWis.ToString();
                    UpdateTotal(-1);
                }
                break;
            case (Abilities.Charisma):
                if (numCha < 3)
                {
                    numCha++;
                    NumCha.text = numCha.ToString();
                    UpdateTotal(-1);
                }
                break;
        }
    }

    private void SubFromStat(Abilities abilitiy)
    {
        if (total == 12)
            return;
        switch (abilitiy)
        {
            case (Abilities.Strength):
                if (numStr > 0)
                {
                    numStr--;
                    NumStr.text = numStr.ToString();
                    UpdateTotal(1);
                }
                break;
            case (Abilities.Dexterity):
                if (numDex > 0)
                {
                    numDex--;
                    NumDex.text = numDex.ToString();
                    UpdateTotal(1);
                }
                break;
            case (Abilities.Constitution):
                if (numCon > 0)
                {
                    numCon--;
                    NumCon.text = numCon.ToString();
                    UpdateTotal(1);
                }
                break;
            case (Abilities.Intelligence):
                if (numInt > 0)
                {
                    numInt--;
                    NumInt.text = numInt.ToString();
                    UpdateTotal(1);
                }
                break;
            case (Abilities.Wisdom):
                if (numWis > 0)
                {
                    numWis--;
                    NumWis.text = numWis.ToString();
                    UpdateTotal(1);
                }
                break;
            case (Abilities.Charisma):
                if (numCha > 0)
                {
                    numCha--;
                    NumCha.text = numCha.ToString();
                    UpdateTotal(1);
                }
                break;
        }
    }
    private void UpdateTotal(int update)
    {
        total += update;
        Total.text = "Total: " + total.ToString();
    }

    private void ActivateCircle(int index)
    {
        for(int i = 0; i< circles.Length; i++)
        {
            if (i == index)
                circles[i].SetActive(true);
            else
                circles[i].SetActive(false);
        }
    }
}
