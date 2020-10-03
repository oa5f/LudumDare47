using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpamPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] int max = 25;
    [SerializeField] TextMeshProUGUI text;

    private bool active = true;
    private int clicks = 0;

    private Action onPuzzleComplete;
    private Action onPuzzleFailed;

    public void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed)
    {
        this.onPuzzleComplete = onPuzzleComplete;
        this.onPuzzleFailed = onPuzzleFailed;
        text.text = $"0 / {max}";
        active = true;
    }

    public void DeactivatePuzzle()
    {
        active = false;
    }
    public void OnButtonPressed()
    {
        if (!active)
            return;
        clicks++;
        text.text = $"{clicks} / {max}";


        if (clicks >= max)
        {
            GetComponent<AudioPlayer>().SpawnSource2D("AnswerCorrect");
            onPuzzleComplete();
        }
        else
            GetComponent<AudioPlayer>().SpawnSource2D("Button");
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
