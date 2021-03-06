﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerPlayerEnterUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent onPlayerEnter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onPlayerEnter.Invoke();
    }
}
