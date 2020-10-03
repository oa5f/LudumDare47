using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPuzzle : MonoBehaviour, IPuzzle
{
    private Action onPuzzleComplete;
    private Action onPuzzleFailed;

    private bool active = true;

    public void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed)
    {
        this.onPuzzleComplete = onPuzzleComplete;
        this.onPuzzleFailed = onPuzzleFailed;
    }

    public void DeactivatePuzzle()
    {
        active = false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void OnPressButton()
    {
        if (active)
        {
            GetComponent<AudioPlayer>().PlaySound("Button");
            onPuzzleComplete();
        }
    }
}
