using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [Header("Sound Data")]
    [SerializeField] private SoundData[] _sounds;

    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play2D(SoundID id)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];
                _audioSource.pitch = Random.Range(0.95f, 1.05f);
                _audioSource.PlayOneShot(clip);
                return;
            }
        }
    }

    public void Play3DAttached(SoundID id, Transform emitter)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];

                AudioSource source = emitter.GetComponent<AudioSource>();

                if (source == null)
                {
                    source = emitter.gameObject.AddComponent<AudioSource>();
                    source.spatialBlend = 1f;
                }

                source.volume = 0.05f;
                source.pitch = Random.Range(0.95f, 1.05f);
                source.PlayOneShot(clip);

                return;
            }
        }
    }

    public void Play3DPooled(SoundID id, Vector3 position)
    {
        foreach (var sound in _sounds)
        {
            if (sound.ID == id)
            {
                if (sound.Clips.Length == 0) return;

                AudioClip clip = sound.Clips[Random.Range(0, sound.Clips.Length)];

                PoolableObject obj = PoolManager.Instance.GetPool(PoolType.POOL_AUDIO_3D).GetObject();
                if (obj is not AudioPoolable audio) return;

                audio.transform.position = position;

                float volume = 0.05f;
                float pitch = Random.Range(0.95f, 1.05f);
                audio.Play(clip, volume, pitch);

                return;
            }
        }
    }
}