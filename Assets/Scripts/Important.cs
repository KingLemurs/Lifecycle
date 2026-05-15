using System;
using UnityEngine;

public class Important : MonoBehaviour
{
    public AudioSource source;

    private bool _triggerFlag = false;
    private int _tickNum = 0;
    private CharacterController controller;

    private void OnTriggerEnter(Collider other)
    {
        source.Play();
        controller = other.gameObject.GetComponent<CharacterController>();
        _triggerFlag = true;
    }

    private void FixedUpdate()
    {
        if (!_triggerFlag || _tickNum >= 10) return;
        _tickNum++;
        controller.Move(new Vector3(0, 100, -3));
    }
}
