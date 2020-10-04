using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance{ get; private set; }

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float showTime = 3f;
    [SerializeField] private LeanTweenType tweenType;

    private bool showing = false;

    private void Start()
    {
        Instance = this;
    }

    public void ShowTutorial(string message)
    {
        if (showing)
            return;

        text.text = message;
        showing = true;

        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, fadeTime).setEase(tweenType).setOnComplete(
            () => LeanTween.move(gameObject, transform.position, showTime).setOnComplete(
                () => LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0f, fadeTime).setEase(tweenType).setOnComplete(
                    () => showing = false)));
    }
}
