using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private int _killRequired;

    private KillManager _killManager;

    protected override void Awake()
    {
        base.Awake();
        _killManager = FindObjectOfType<KillManager>();
    }

    protected override void Open()
    {
        base.Open();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(Tags.Player)) return;

        if (_killManager.HasReachedKills(_killRequired))
        {
            _canInteract = true;
            Debug.Log("Premi E per aprire la porta");
        }
        else
        {
            Debug.Log($"Hai bisogno di {_killRequired} uccisioni per aprirla, uccidi altri nemici o ricomincia il livello!");
        }
    }
}
