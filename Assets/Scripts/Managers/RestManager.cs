using UnityEngine;
using System.Linq;

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
        GameManager.Instance.LoadScene("MainMap");
    }
    
}
