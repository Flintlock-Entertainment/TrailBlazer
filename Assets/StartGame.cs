using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
   public void LoadScene()
    {
        PlayerPrefs.DeleteKey("Map");
        PlayerPrefs.DeleteKey("Heroes_Saved");
        GameManager.Instance.LoadScene("CharacterCreation");
    }
}
