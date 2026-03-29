using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : GenericSingleton<ParticleManager>
{
    [Header("Effects")]
    [SerializeField] private List<ParticleEntry> _particles;

    private Dictionary<ParticleType, ParticleSystem> _particleDictionary;

    override protected void Awake()
    {
        base.Awake();

        _particleDictionary = new Dictionary<ParticleType, ParticleSystem>();
        MapDictionary();
    }

    private void MapDictionary()
    {
        foreach (ParticleEntry entry in _particles)
        {
            _particleDictionary[entry.ParticleType] = entry.Particle;
        }
    }

    public void Play(ParticleType type, Transform parent, bool destroyAfterPlay = true)
    {
        if (!_particleDictionary.TryGetValue(type, out var particle)) return;

        ParticleSystem newParticle = Instantiate(particle, parent.position, Quaternion.identity, parent);
        newParticle.Play();
        if (destroyAfterPlay) Destroy(newParticle.gameObject, newParticle.main.duration + newParticle.main.startLifetime.constantMax);
    }
}
