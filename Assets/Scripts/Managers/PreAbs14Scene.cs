using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreAbs14Scene : MonoBehaviour
{
    public void Agree()
    {
        SceneManager.LoadScene("Abs(14)");
    }
    public void Disagree()
    {
        SceneManager.LoadScene("MainMap");
    }
}
