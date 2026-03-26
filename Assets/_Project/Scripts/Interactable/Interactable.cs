using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected AnimationParamHandler _animHandler;
    protected bool _canInteract;

    protected virtual void Awake()
    {
        _animHandler = GetComponent<AnimationParamHandler>();
    }

    protected virtual void Open()
    {
        _animHandler.SetIsOpen();
    }

    protected virtual void Update()
    {
        if (Input.GetButtonDown(Inputs.E) && _canInteract)
        {
            Open();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;

        _canInteract = false;
    }

}
