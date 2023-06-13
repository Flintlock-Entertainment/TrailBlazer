using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatLogger : MonoBehaviour
{
    // Inspector fields
    [SerializeField] private int maxLogLines;
    [SerializeField] private TextMeshProUGUI logText;

    // Private fields
    private Queue<string> messageQueue;

    void Awake()
    {
        messageQueue = new Queue<string>();
        logText.text = "\tCombat has started!\n";
    }

    // Add a message to the combat log
    public void AddLog(string message)
    {
        logText.text += message;
        // Enqueue the message
       // messageQueue.Enqueue(message);

        // If the queue is too long, remove the oldest message
       // while (messageQueue.Count > maxLogLines)
       // {
       //     messageQueue.Dequeue();
       // }

        // Update the log text
       // UpdateLogText();
    }

    // Update the combat log text
    private void UpdateLogText()
    {
        // Clear the log text
        logText.text = "";

        // Add each message to the log text
        foreach (string message in messageQueue)
        {
            logText.text += message + "\n";
        }

        // Set the anchored position of the log text to the top of its parent rect transform
        logText.rectTransform.anchoredPosition = new Vector2(logText.rectTransform.anchoredPosition.x, 0f);
    }
}
