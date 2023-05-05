using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InformationMenu : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI HPText;
    [SerializeField] public TextMeshProUGUI speedText;

    public void init(int HP, int speed)
    {
        HPText.text = "HP: " + HP;
        speedText.text = "Speed: " + speed+ " squares";
    }

    public void ChangeHP(int HP)
    {
        HPText.text = "HP: " + HP;
    }

}
