using UnityEngine;

public class EvadingEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float evadeDistance = 5f;
    public LayerMask obstacleLayer;
    private bool facingRight = true;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Calculate direction away from the player
        Vector2 directionAway = (transform.position - player.position).normalized;

        // Check for obstacles directly in the evade path
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionAway, evadeDistance, obstacleLayer);
        if (hit.collider != null)
        {
            // If an obstacle is detected, choose a perpendicular direction
            directionAway = Vector3.Cross(directionAway, Vector3.forward).normalized;
        }

        // Randomly adjust the direction slightly to prevent predictable paths
        directionAway += new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized * 0.2f;
        directionAway.Normalize();

        if (directionAway.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (directionAway.x < 0 && facingRight)
        {
            Flip();
        }

        // Move away from the player
        rb.linearVelocity = directionAway * speed;

        // Boundary check to keep within certain area (optional implementation goes here)
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}