using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InfoScreen : MonoBehaviour
{
    public static InfoScreen Instance;
    [SerializeField] private TextMeshPro Text;
    [SerializeField] private GameObject Screen;
    [SerializeField] private Button continueBtn;

    [TextArea(10, 10)]
    [SerializeField] private string text;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowInfo()
    {
        Text.gameObject.SetActive(true);
        Screen.gameObject.SetActive(true);
        continueBtn.gameObject.SetActive(true);
        Text.text = text;
    }

    public void HideInfo()
    {
        Text.gameObject.SetActive(false);
        Screen.gameObject.SetActive(false);
        continueBtn.gameObject.SetActive(false);
        Text.text = "";
    }

}
