using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] LeanTweenType tweenType;
    [SerializeField] private float time = 1.5f;
    [SerializeField] private Vector3 endPoint;

    public void Close()
    {
        LeanTween.moveLocal(gameObject, endPoint, time).setEase(tweenType);
    }
}
