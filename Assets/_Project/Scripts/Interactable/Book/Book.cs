using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Interactable
{
    [SerializeField] private Transform _secretEntrance;

    protected override void Open()
    {
        base.Open();
        Destroy(GetComponent<Collider2D>());
        Destroy(_secretEntrance.gameObject);        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;

        _canInteract = true;

        Debug.Log("Premi E per aprire e leggere il libro");
    }
}
