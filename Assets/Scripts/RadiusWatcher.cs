using UnityEngine;

/**
 * This component makes its object watch a given radius, and if the target is found - it starts chasing it.
 */
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Patroller))]
public class RadiusWatcher : MonoBehaviour
{
    [SerializeField] float radiusToWatch = 5f;

    private Chaser chaser;
    private Patroller patroller;
    private void Start()
    {
        chaser = GetComponent<Chaser>();
        chaser.enabled = false;

        patroller = GetComponent<Patroller>();
        patroller.enabled = true;
    }

    void Update()
    {
        float distanceToTarget = Vector3.Distance(
            transform.position, chaser.TargetObjectPosition());
        if (distanceToTarget <= radiusToWatch)
        {
            chaser.enabled = true;
            patroller.enabled = false;

        }
        else
        {
            chaser.enabled = false;
            patroller.enabled = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enabled)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radiusToWatch);
        }
    }
}