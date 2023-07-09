using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    //private PlatformerInputActions platformerInputActions;
    private IInteractable interactible;

    private void Awake()
    {
        //platformerInputActions=new PlatformerInputActions();
    }
    private void OnEnable()
    {
        //platformerInputActions.Player.Interact.performed += Interact;
        //platformerInputActions.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        //platformerInputActions.Player.Interact.Disable();
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (interactible != null && Input.GetButtonDown("Interact"))
        {
            interactible.Interact();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        interactible = collision.gameObject.GetComponent<IInteractable>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactible = null;
    }
}
