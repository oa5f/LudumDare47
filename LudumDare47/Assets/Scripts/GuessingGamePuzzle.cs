using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuessingGamePuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private int correctAnswer;

    [SerializeField] private TextMeshProUGUI numberDisplay;
    [SerializeField] private TextMeshProUGUI feedBackText;

    private Action onPuzzleComplete;
    private Action onPuzzleFailed;

    private bool active = true;
    private int enteredNumber = 0;
    private bool isResetting = false;

    private AudioPlayer audioPlayer;


    public void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed)
    {
        active = true;
        this.onPuzzleComplete = onPuzzleComplete;
        this.onPuzzleFailed = onPuzzleFailed;
        UpdateDisplay();
        feedBackText.text = string.Empty;
        audioPlayer = GetComponent<AudioPlayer>();
    }

    public void DeactivatePuzzle()
    {
        active = false;
    }
    public void OnNumberButtonPress(int button)
    {
        if (isResetting)
        {
            enteredNumber = 0;
            isResetting = false;
        }
        if (!active)
            return;
        if(button > 9 || button < 0)
        {
            Debug.LogError($"Button {button} out of range in guessing game", this);
            return;
        }
        audioPlayer.PlaySound("Button");
        if (enteredNumber > 10000)
            return;
        if (enteredNumber == 0)
            enteredNumber = button;
        else
            enteredNumber = enteredNumber * 10 + button;

        UpdateDisplay();
    }
    public void OnPressSubmit()
    {
        if (!active)
            return;
        if(enteredNumber == correctAnswer)
        {
            audioPlayer.SpawnSource2D("AnswerCorrect");
            feedBackText.text = "Correct!";
            onPuzzleComplete();
            return;
        }

        audioPlayer.PlaySound("Button");
        
        feedBackText.text = enteredNumber > correctAnswer ? "Lower" : "Higher";
        isResetting = true;
    }

    private void UpdateDisplay()
    {
        numberDisplay.text = enteredNumber.ToString();
        
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
