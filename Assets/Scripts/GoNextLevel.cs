using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Manages the player's collisions with various objects in the game.
 * Handles scoring, level transitions, and player destruction.
 */
public class GoNextLevel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Tag for objects.")]
    private string next;
    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == next && enabled)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            Destroy(other.gameObject);
        }
    }
}