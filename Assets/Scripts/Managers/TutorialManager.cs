using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;


public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshPro tutorialText;

    // an array of game objects to show different pop-ups
    [SerializeField] public GameObject[] popUps;

    // an array of strings to store the text for each pop-up
    [SerializeField] public string[] popUpText;

    // an index to keep track of which pop-up is currently being shown
    private int popUpIndex;

    // input action for left mouse button click
    [SerializeField] InputAction lmbClick;

    [SerializeField] GameObject TextSceen;

    // make sure the input action is properly initialized
    void OnValidate()
    {
        if (lmbClick == null)
            lmbClick = new InputAction(type: InputActionType.Button);
        if (lmbClick.bindings.Count == 0)
            lmbClick.AddBinding("<Mouse>/leftButton");
    }

    // enable the input action when the object is enabled
    private void OnEnable()
    {
        lmbClick.Enable();
    }

    // disable the input action when the object is disabled
    private void OnDisable()
    {
        lmbClick.Disable();
    }

    void Start()
    {
        // show the first pop-up and hide all others
        tutorialText.text = popUpText[0];
        popUps[0].SetActive(true);
        for (int i = 1; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
    }

    void Update()
    {
        // when left mouse button is clicked, show the next pop-up and hide all others
        if (lmbClick.WasPerformedThisFrame())
        {
            popUpIndex++;
            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[i].SetActive(true);
                    tutorialText.text = popUpText[i];
                }
                else
                {
                    popUps[i].SetActive(false);
                }
            }
        }
        // if all pop-ups have been shown, destroy the tutorial objects and the tutorial manager
        if (popUpIndex == popUpText.Length)
        {
            for (int i = 0; i < popUps.Length; i++)
            {
                Destroy(popUps[i]);
            }
            Destroy(tutorialText.gameObject);
            Destroy(TextSceen);
            Destroy(this.gameObject);
        }
    }
}
