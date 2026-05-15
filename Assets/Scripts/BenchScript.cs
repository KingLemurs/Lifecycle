using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BenchScript : MonoBehaviour
{
    public Image Image;
    public GameObject UI;
    public Transform sitPos;
    public GameObject player;
    public CharacterController controller;

    private PlayerInputs _actions;
    private InputAction _interact;
    private bool _entered = false;

    private bool _panelFlag = false;

    private int _tickNum = -1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _actions = new();
    }

    private void OnEnable()
    {
        _interact = _actions.Player.Interact;
        _interact.Enable();
        _interact.performed += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        print("fart poop pee pee");
        if (!_entered) return;

        controller.enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.transform.position = sitPos.position;
        UI.SetActive(false);
        _entered = false;
        Invoke(nameof(Enablepanel), 10);
    }

    private void Enablepanel()
    {
        _panelFlag = true;
    }

    private void FixedUpdate()
    {
        _tickNum++;
        if (Image.color.a >= 1.5f)
        {
            print("load cemetery");
        }
        
        if (!_panelFlag || _tickNum % 8 != 0) return;

        print(Image.color.a);
        var color = Image.color;
        color.a = color.a + 0.01f;
        Image.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        _entered = true;
        UI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        _entered = false;
        UI.SetActive(false);
    }
}
