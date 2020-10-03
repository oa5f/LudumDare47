using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPuzzle
{
    void ActivatePuzzle(Action onPuzzleComplete, Action onPuzzleFailed);
    void DeactivatePuzzle();
    GameObject GetGameObject();
}
