using System.Collections;
using UnityEngine;

public class AudioPoolable : PoolableObject
{
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public override void OnSpawned()
    {
        _source = GetComponent<AudioSource>();
        if (_source == null)
        {
            _source = gameObject.AddComponent<AudioSource>();
            _source.spatialBlend = 1f;
        }
    }

    public override void OnDespawned()
    {
        _source.Stop();
    }

    public void Play(AudioClip clip, float volume, float pitch)
    {
        _source.volume = volume;
        _source.pitch = pitch;
        _source.PlayOneShot(clip);

        StartCoroutine(ReturnAfterPlayer(clip.length));
    }

    private IEnumerator ReturnAfterPlayer(float duration)
    {
        yield return new WaitWhile(() => _source.isPlaying);

        Release();
    }
}