using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CombatLogger : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI logger;

    public void Start()
    {
        logger.text = "";
    }
    public void AddLog(string log)
    {
        logger.text += log;
    }
}