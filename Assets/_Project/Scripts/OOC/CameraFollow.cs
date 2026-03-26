

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 newPos = _target.position;
            newPos.z = transform.position.z;
            transform.position = newPos;
        }
    }
}
