using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textWindow;
    [SerializeField] string text;
    private void OnMouseEnter()
    {
        textWindow.text = text;
    }

    private void OnMouseExit()
    {
        textWindow.text = "";
    }
}
