using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{
    public void Reincarnate()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
}
