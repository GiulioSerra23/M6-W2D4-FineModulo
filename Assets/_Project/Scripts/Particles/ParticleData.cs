using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ParticleType
{    
    NONE = 0,

    PARTICLE_JUMP_DUST = 1,

    PARTICLE_BULLET_LOG = 10,

    PARTICLE_PICKUP_COIN = 20,

    PARTICLE_RUNE = 30,
}

[System.Serializable]
public class ParticleEntry
{
    [SerializeField] private ParticleType _particleType;
    [SerializeField] private ParticleSystem _particle;

    public ParticleType ParticleType => _particleType;
    public ParticleSystem Particle => _particle;
}
