using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticlesTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Particle Type")]
    [SerializeField] private ParticleType _particleType;
    [SerializeField] private bool _destroyAfterPlay = false;

    public void TriggerEnter(Collider other)
    {
        ParticleManager.Instance.Play(_particleType, transform, _destroyAfterPlay);
    }

    public void TriggerExit(Collider other) { }
}
