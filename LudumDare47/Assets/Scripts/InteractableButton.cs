using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractableButton : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private Color selectedColor;

    private Color defaultColor;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        defaultColor = mat.color;
    }

    public void Interact()
    {
        onPress.Invoke();
    }

    public void OnDeselected()
    {
        mat.color = defaultColor;
    }

    public void OnSelected()
    {
        mat.color = selectedColor;
    }

    private void OnDestroy()
    {
        // Avoid memory leak
        Destroy(mat);
    }
}
