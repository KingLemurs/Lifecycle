using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform cameraPosition;

    private void Update()
    {
        transform.position = cameraPosition.transform.position;
    }
}
