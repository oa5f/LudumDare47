using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;

    public float TimeLeft = 60f;

    public int roomIndex = 0;

    [SerializeField] private Door exit;
    [SerializeField] private GameObject[] puzzlePrefabs;
    [SerializeField] private Transform puzzleParent;

    [SerializeField] private float puzzleTweenDuration = 3f;
    [SerializeField] LeanTweenType puzzleTweenInType;
    [SerializeField] LeanTweenType puzzleTweenOutType;

    [SerializeField] GameObject enableIfFirst;


    private int currentPuzzleIndex = -1;
    private bool loaded;
    private Action onRoomComplete;
    private Action onRoomFail;

    private IPuzzle currentPuzzle;

    private void Update()
    {
        if(loaded)
            TimeLeft = Mathf.Clamp(TimeLeft - Time.deltaTime, 0f, Mathf.Infinity);

        if (loaded && TimeLeft < Mathf.Epsilon)
            onRoomFail();
    }
    public void OpenExit()
    {
        loaded = false;
        DisablePuzzle(currentPuzzle);
        exit.Close();
    }
    public void OnFirstRoomLoaded()
    {
        enableIfFirst.SetActive(true);
    }
    public void OnRoomLoaded(Action onRoomComplete, Action onRoomFail)
    {
        loaded = true;
        this.onRoomComplete = onRoomComplete;
        this.onRoomFail = onRoomFail;

        SpawnNextPuzzle();
    }
    public void Unload()
    {
        loaded = false;
    }
    private void SpawnNextPuzzle()
    {
        currentPuzzleIndex++;

        if(currentPuzzleIndex >= puzzlePrefabs.Length)
        {
            onRoomComplete();
            return;
        }

        GameObject spawnedPuzzle = Instantiate(puzzlePrefabs[currentPuzzleIndex], puzzleParent);
        currentPuzzle = spawnedPuzzle.GetComponent<IPuzzle>();
        currentPuzzle.ActivatePuzzle(OnPuzzleCompleted, OnPuzzleFailed);

        spawnedPuzzle.transform.localPosition = Vector3.up * 5f;
        LeanTween.moveLocal(spawnedPuzzle, Vector3.zero, puzzleTweenDuration).setEase(puzzleTweenInType);
    }
    private void OnPuzzleCompleted()
    {
        DisablePuzzle(currentPuzzle);
        SpawnNextPuzzle();
    }
    private void DisablePuzzle(IPuzzle puzzle)
    {
        puzzle.DeactivatePuzzle();
        LeanTween.moveLocal(puzzle.GetGameObject(), Vector3.down * 5f, puzzleTweenDuration).setEase(puzzleTweenOutType).destroyOnComplete = true;
    }
    private void OnPuzzleFailed()
    {
        onRoomFail();
    }
}
