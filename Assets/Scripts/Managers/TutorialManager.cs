using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TextMeshPro tutorialText;
    [SerializeField] public GameObject[] popUps = new GameObject[4];
    [SerializeField] public string[] popUpText = new string[4];
    private int popUpIndex;
    [SerializeField] InputAction lmbClick;
    

    void OnValidate()
    {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (lmbClick == null)
            lmbClick = new InputAction(type: InputActionType.Button);
        if (lmbClick.bindings.Count == 0)
            lmbClick.AddBinding("<Mouse>/leftButton");
    }

    private void OnEnable()
    {
        lmbClick.Enable();
    }

    private void OnDisable()
    {
        lmbClick.Disable();
    }

    void Start()
    {
        tutorialText.text = popUpText[0];
        popUps[0].SetActive(true);
        for (int i = 1; i < popUps.Length; i++)
        {
            popUps[i].SetActive(false);
        }
    }

    void Update()
    {
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
        if(popUpIndex == popUpText.Length)
        {
            for (int i = 0; i < popUps.Length; i++)
            {
                Destroy(popUps[i]);
            }
            Destroy(tutorialText);
            //tutorialText.text = "";
            Destroy(this.gameObject);

        }
    }
}
