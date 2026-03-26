using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    protected Vector2 _newPos;
    protected float _offSet = 0.8f;

    protected void Start()
    {
        _newPos = new Vector2(transform.position.x, transform.position.y + _offSet);
    }

    protected virtual void Drop() { }

    protected override void Open()
    {
        base.Open();
        Drop();
        GetComponent<Collider2D>().enabled = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;

        _canInteract = true;

        Debug.Log("Premi E per aprire la chest");
    }
}
