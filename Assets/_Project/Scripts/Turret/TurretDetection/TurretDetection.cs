
using UnityEngine;

public class TurretDetection : MonoBehaviour
{
    public Transform Target { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;

        Target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;

        Target = null;
    }
}
