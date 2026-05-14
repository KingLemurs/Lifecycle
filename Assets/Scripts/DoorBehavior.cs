using UnityEngine;
using LifeCycle;

public class DoorBehavior : MonoBehaviour, IInteractable
{
    private bool opened;

    public float openAngle = 111f;
    public float speed = 2f;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private Quaternion targetRotation;

    private void Start()
    {
        closedRotation = transform.rotation;

        openedRotation = Quaternion.Euler(
            transform.eulerAngles + new Vector3(0, openAngle, 0));

        targetRotation = closedRotation;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            Time.deltaTime * speed);
    }

    public void OnInteract()
    {
        opened = !opened;

        if (opened)
            Open();
        else
            Close();
    }

    private void Open()
    {
        targetRotation = openedRotation;
    }

    private void Close()
    {
        targetRotation = closedRotation;
    }
}