using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InformationMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI unitDesc;
    [SerializeField] public TextMeshProUGUI turnCounterText;

    public void init(string desc, int turns)
    {
        unitDesc.text = desc;
        turnCounterText.text = "Actions Left: " + turns + "/3";
    }
    public void ChangeTurnCounter(int turns)
    {
        turnCounterText.text = "Actions Left: " + turns + "/3";
    }

    public void ShowInformation(string desc)
    {
        unitDesc.text = desc;
    }
}
