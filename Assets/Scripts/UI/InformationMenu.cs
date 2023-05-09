using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InformationMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI HPText;
    [SerializeField] public TextMeshProUGUI speedText;
    [SerializeField] public TextMeshProUGUI turnCounterText;

    public void init(int HP, int speed, int turns)
    {
        HPText.text = "HP: " + HP;
        speedText.text = "Speed: " + speed+ " squares";
        turnCounterText.text = "Actions Left: " + turns + "/3";
    }

    public void ChangeHP(int HP)
    {
        HPText.text = "HP: " + HP;
    }
    public void ChangeTurnCounter(int turns)
    {
        turnCounterText.text = "Actions Left: " + turns + "/3";
    }

}
