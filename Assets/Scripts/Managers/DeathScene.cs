using UnityEngine;


public class DeathScene : MonoBehaviour
{
    public void Reincarnate()
    {
        GameManager.Instance.LoadScene("CharacterCreation");
    }
}
