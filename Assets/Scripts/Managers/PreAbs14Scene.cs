using UnityEngine;


public class PreAbs14Scene : MonoBehaviour
{
    public void Agree()
    {
        GameManager.Instance.LoadScene("Abs(14)");
    }
    public void Disagree()
    {
        GameManager.Instance.LoadScene("MainMap");
    }
}
