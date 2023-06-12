using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

using UnityEngine.SceneManagement;
public class RestManager : MonoBehaviour
{
    void Start()
    {
        var _units = Resources.LoadAll<ScriptableUnit>("Units").ToList();
        var character = _units.Where(u => u.Faction == Faction.Character).First();
        character.SetCurrentHP(character.GetHP());
    }

    public void BackToMap()
    {

    }
    
}
