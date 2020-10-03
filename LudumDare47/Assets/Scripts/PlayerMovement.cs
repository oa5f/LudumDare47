using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;

    [SerializeField] private float speed = 3f;
    [SerializeField] private float moveLerpValue;

    [SerializeField] private Transform cam;

    [SerializeField] private PauseMenu pauseMenu;

    public float MouseSensitivity { get; set; } = 1f;
    private const float sensitivityMultiplier = 0.35f;

    private Vector2 input = Vector2.zero;
    private float cameraXRotation = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (RoomFailedManager.Showing)
            return;

        Move();

        if(!pauseMenu.IsPaused)
            Rotate();

    }
    private void Move()
    {

        Vector2 currentInput = Vector2.right * Input.GetAxisRaw("Horizontal") + Vector2.up * Input.GetAxisRaw("Vertical");
        input = Vector2.Lerp(input, currentInput, moveLerpValue * Time.deltaTime);

        Vector3 localInput = transform.forward * input.y + transform.right * input.x;
        float localInputMagnitude = localInput.magnitude;
        localInput.y = 0f;
        localInput.Normalize();
        localInput *= localInputMagnitude;

        characterController.Move(localInput * speed * Time.deltaTime);
    }
    private void Rotate()
    {
        // Rotate
        Vector2 mouseMovement = new Vector2(Input.GetAxisRaw("Mouse X"), -Input.GetAxisRaw("Mouse Y")) * MouseSensitivity * sensitivityMultiplier;
        transform.Rotate(new Vector3(0f, mouseMovement.x, 0f));
        
        cameraXRotation = Mathf.Clamp(cameraXRotation + mouseMovement.y, -90f, 90f);
        cam.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);

    }
}