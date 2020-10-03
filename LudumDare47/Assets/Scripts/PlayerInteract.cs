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

        RaycastHit[] hits = Physics.RaycastAll(center.position, center.forward, maxDistance, mask);

        bool hasHit = false;

        RaycastHit hit = new RaycastHit();
        foreach (RaycastHit i in hits)
        {
            if (i.collider.GetComponent<IInteractable>() != null)
            {
                hit = i;
                hasHit = true;
            }
        }

        if(!hasHit || hit.collider.GetComponent<IInteractable>() == null)
        {
            if (selected != null)
                selected.OnDeselected();
            selected = null;
            return;
        }
        var interactable = hit.collider.GetComponent<IInteractable>();
        if (selected != null && !(interactable == selected))
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
