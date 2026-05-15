using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class FenceScript : MonoBehaviour
{
    //public Image Image;
    public GameObject UI;
    public Transform jumpPos;
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
        if (!_entered) return;

        controller.enabled = false;
        player.GetComponent<PlayerController>().enabled = false;

        UI.SetActive(false);
        _entered = false;

        StartCoroutine(JumpArc());
        Invoke(nameof(Enablepanel), 10);
    }

    private IEnumerator JumpArc()
    {
        Vector3 startPos = player.transform.position;
        Vector3 endPos = jumpPos.position;
        float duration = 0.6f;   // tweak: total jump time in seconds
        float arcHeight = 1.5f;  // tweak: how high the arc goes
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Lerp horizontal position
            Vector3 currentPos = Vector3.Lerp(startPos, endPos, t);

            // Add parabolic vertical offset: peaks at t=0.5, zero at t=0 and t=1
            currentPos.y += arcHeight * (1f - (2f * t - 1f) * (2f * t - 1f));

            player.transform.position = currentPos;
            yield return null;
        }

        // Snap to exact end position
        player.transform.position = endPos;

        controller.enabled = true;
        player.GetComponent<PlayerController>().enabled = true;
    }

    private void Enablepanel()
    {
        _panelFlag = true;
    }

    private void FixedUpdate()
    {
        _tickNum++;
        //.color.a >= 1.5f)
        //{
        //    print("load cemetery");
       // }
        
        if (!_panelFlag || _tickNum % 8 != 0) return;

       // print(Image.color.a);
       // var color = Image.color;
       // color.a = color.a + 0.01f;
       // Image.color = color;
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
