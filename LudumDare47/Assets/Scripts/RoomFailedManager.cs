using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFailedManager : MonoBehaviour
{
    [SerializeField] private float tweenDuration = 1f;
    [SerializeField] private float timeVisible = 1f;
    [SerializeField] LeanTweenType tweenType;

    public static bool Showing { get; private set; }


    private void Start()
    {
        Showing = false;
    }
    public void Show()
    {
        // It Works!
        Showing = true;
        LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1f, tweenDuration).setEase(tweenType).setOnComplete(
            () => LeanTween.move(gameObject, transform.position, timeVisible).setOnComplete(
                () => LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0f, tweenDuration).setEase(tweenType).setOnComplete(
                    () => Showing = false)));
    }

    private void OnDestroy()
    {
        Showing = false;
    }
}
