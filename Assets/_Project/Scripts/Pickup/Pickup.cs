
using UnityEngine;

public abstract class Pickup : MonoBehaviour, IPickable
{
    [Header ("Sound ID")]
    [SerializeField] private SoundID _pickupSound;

    [Header("Particle Type")]
    [SerializeField] protected ParticleType _pickupParticle;

    private bool _isPicked = false;

    public virtual void OnPick(GameObject picker)
    {
        _isPicked = true;
        AudioManager.Instance.Play2D(_pickupSound);
        ParticleManager.Instance.Play(_pickupParticle, transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;

        if (_isPicked) return;
        
        OnPick(other.gameObject);
        Destroy(gameObject);
    }
}
