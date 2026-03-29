
using UnityEngine;

public class BounceBullet : Bullet
{
    protected override void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play3DPooled(_collisionSound, transform.position);

        if (collision.collider.CompareTag(Tags.Player))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectVelocity = Vector3.Reflect(_rb.velocity, normal);

            _rb.velocity = reflectVelocity;
        }
        else
        {
            Release();            
        }        
    }
}
