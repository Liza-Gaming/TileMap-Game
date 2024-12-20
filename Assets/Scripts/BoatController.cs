using UnityEngine;

public class BoatController : MonoBehaviour
{
    public GameObject player; // Assign your player GameObject in the inspector
    public GameObject playerSprite; // Assign the player sprite GameObject in the inspector
    public Transform boatSeat; // The position on the boat where the player sits

    private bool isPlayerOnBoat = false;
    private Animator playerAnimator;



    void Start()
    {
        playerAnimator = player.GetComponent<Animator>(); // Get the Animator component
        GetComponent<BoatMovement>().enabled = false; // Ensure boat movement is initially disabled
    }

    void Update()
    {
        if (isPlayerOnBoat)
        {
            playerSprite.transform.position = boatSeat.position; // Keep player on boat
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
            // Player gets on the boat
            isPlayerOnBoat = true;
            player.GetComponent<Move>().enabled = false; // Disable player movement
            playerSprite.transform.SetParent(transform); // Attach player sprite to the boat
            GetComponent<BoatMovement>().enabled = true; // Enable boat controls

            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("horizontal", 0f);
                playerAnimator.SetFloat("vertical", 0f); // Force the animator to idle state
            }
        }
    }

    public void OnPlayerExitBoat()
    {
        // Implement logic for player to leave the boat
        isPlayerOnBoat = false;
        player.GetComponent<Move>().enabled = true; // Re-enable player movement
        playerSprite.transform.SetParent(null); // Detach player sprite from the boat
        playerSprite.transform.position = boatSeat.position + new Vector3(1f, 0f, 0f); // Adjust as needed
        GetComponent<BoatMovement>().enabled = false; // Disable boat controls
    }
}