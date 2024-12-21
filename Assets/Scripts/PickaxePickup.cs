using UnityEngine;

public class PickaxePickup : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag for objects.")]
    private string playerTag;

    [SerializeField]
    [Tooltip("Tag for objects.")]
    private GameObject player;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == playerTag && enabled)
        {
            // Assuming your Player script handles state changes
            PickaxeEffect playerScript = player.GetComponent<PickaxeEffect>();
            if (playerScript != null)
            {
                playerScript.EnableElevationMovement();
                Destroy(gameObject); // Remove the pickaxe after picking up
            }
        }
    }
}