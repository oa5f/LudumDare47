using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private List<GameObject> items;

    private bool active = true;

    private Action onPuzzleComplete;
    private Action onPuzzleFailed;

    public void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed)
    {
        this.onPuzzleComplete = onPuzzleComplete;
        this.onPuzzleFailed = onPuzzleFailed;
    }

    public void DeactivatePuzzle()
    {
        active = false;
        foreach(GameObject item in items)
        {
            Destroy(item);
        }
    }
    public void OnItemCollect(GameObject item)
    {
        if (!active)
            return;

        items.Remove(item);
        Destroy(item);

        if(items.Count <= 0)
        {
            onPuzzleComplete();
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
