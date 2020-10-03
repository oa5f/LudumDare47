using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Transform center;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask mask;

    private IInteractable selected;
    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (selected != null)
                selected.Interact();
        }
    }
    private void FixedUpdate()
    {
        RaycastHit[] hit = Physics.RaycastAll(center.position, center.forward, maxDistance, mask);
        if(hit.Length == 0 || hit[0].collider.GetComponent<IInteractable>() == null)
        {
            if (selected != null)
                selected.OnDeselected();
            selected = null;
            return;
        }
        var interactable = hit[0].collider.GetComponent<IInteractable>();
        if (selected != null && !interactable.Equals(selected))
        {
            selected.OnDeselected();
            selected = null;
        }
        if(selected == null)
        {
            selected = interactable;
            selected.OnSelected();
            return;
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (center == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(center.position, center.position + center.forward * maxDistance);
    }
#endif
}
