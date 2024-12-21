using UnityEngine;

public class RideableController : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the inspector
    public GameObject playerSprite; // Assign the player sprite GameObject in the inspector
    public Transform seat; // The position on where the player sits

    private bool isPlayerOn = false;
    private Animator playerAnimator;



    void Start()
    {
        playerAnimator = player.GetComponent<Animator>(); // Get the Animator component
        GetComponent<RideableMovement>().enabled = false; // Ensure rideable movement is initially disabled
    }

    void Update()
    {
        if (isPlayerOn)
        {
            playerSprite.transform.position = seat.position; // Keep player on the rideable
            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("horizontal", 0f);
                playerAnimator.SetFloat("vertical", 0f); // Set animator parameters to zero for Idle state
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Player gets on the rideable
            isPlayerOn = true;
            player.GetComponent<Move>().enabled = false; // Disable player movement
            playerSprite.transform.SetParent(transform); // Attach player sprite to the rideable
            GetComponent<RideableMovement>().enabled = true; // Enable rideable controls

            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("horizontal", 0f);
                playerAnimator.SetFloat("vertical", 0f); // Force the animator to idle state
            }
        }
    }
}