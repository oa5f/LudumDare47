using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSelectionPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private int length = 5;

    [SerializeField] Renderer[] buttons;

    [SerializeField] TextMeshProUGUI text;

    [SerializeField] int[] rightAnwsers;


    private bool active = true;

    private Action onPuzzleComplete;
    private Action onPuzzleFailed;
    private int round = -1;

    private int currentRightButton = 0;

    public void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed)
    {
        this.onPuzzleComplete = onPuzzleComplete;
        this.onPuzzleFailed = onPuzzleFailed;

        NextRound();
    }

    public void DeactivatePuzzle()
    {
        active = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnButtonPress(int button)
    {
        if (!active)
            return;

        if (button == currentRightButton)
        {
            GetComponent<AudioPlayer>().PlaySound("AnswerCorrect");
            NextRound();
        }
        else
        {
            GetComponent<AudioPlayer>().PlaySound("Fail");
            onPuzzleFailed();
        }
    }
    private void NextRound()
    {
        round++;
        if (round >= length)
        {
            text.text = string.Empty;
            onPuzzleComplete();
            return;
        }
        currentRightButton = rightAnwsers[round];

        string displayText = string.Empty;
        for (int i = 0; i < buttons.Length; i++)
        {
            displayText += currentRightButton == i ? "O" : "X";
            displayText += "  ";
            if (i == 1)
                displayText += "\n";
        }
        text.text = displayText;
    }

}
