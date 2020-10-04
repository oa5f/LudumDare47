using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Gradient gradient;
    [SerializeField] private UnityEvent onCollect;

    private Material material;
    private Color color;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        color = gradient.Evaluate(Random.value);
        SetColor(color);
        GetComponent<AudioPlayer>().PlaySound("Item", true);
    }

    public void Interact()
    {
        GetComponent<AudioPlayer>().SpawnSource2D("Button");
        onCollect.Invoke();
    }

    public void OnDeselected()
    {
        if (material != null)
            SetColor(color);
    }
    private void SetColor(Color c)
    {
        material.SetColor("_BaseColor", c);
    }

    public void OnSelected()
    {
        Color selectedColor = color;
        Color.RGBToHSV(selectedColor, out float h, out _, out float v);
        float s = 1f;
        selectedColor = Color.HSVToRGB(h, s, v);
        SetColor(selectedColor);
    }

    private void OnDestroy()
    {
        // Avoid memory leak
        Destroy(material);
    }
}
