using UnityEngine;


public class WinScene : MonoBehaviour
{
    public void Reincarnate()
    {
        GameManager.Instance.LoadScene("CharacterCreation");
    }
}
