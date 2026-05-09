using UnityEngine;
using LifeCycle;

public class Raycast : MonoBehaviour
{
    public float maxDistance;
    int mask;

    private void Start()
    {
        mask = ~LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Cast();
        }
    }

    private void Cast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, mask))
        {
            Debug.Log(hit.collider.name);
            hit.collider.GetComponent<IInteractable>()?.OnInteract();
        }
    }
}
