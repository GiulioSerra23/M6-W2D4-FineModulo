
using UnityEngine;

public class BounceBullet : Bullet
{
    protected override void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.Play(_collisionSound);
        if (collision.collider.CompareTag(Tags.Player))
        {
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectVelocity = Vector3.Reflect(_rb.velocity, normal);

            _rb.velocity = reflectVelocity;
        }
        else
        {
            gameObject.SetActive(false);
        }            
    }
}
