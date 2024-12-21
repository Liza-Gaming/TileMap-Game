using UnityEngine;
using System.Collections.Generic;

public class EvadingEnemy : MonoBehaviour
{
    public Transform player; // Must be assigned in tests or Unity
    public float speed = 5f;
    public float evadeDistance = 5f;
    public LayerMask obstacleLayer;
    public bool facingRight = true;

    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Make sure rb is assigned here
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is not assigned!");
        }
    }

    public void Update()
    {
        if (player == null) return; // Check if player is assigned
        Vector2 directionAway = (transform.position - player.position).normalized;

        // Calculate potential escape routes from the player
        List<Vector2> potentialDirections = GetEscapeDirections(directionAway);

        // Choose a direction to evade based on highest quality (i.e., least collision)
        Vector2 bestDirection = FindBestEvadeDirection(potentialDirections);
        MoveInDirection(bestDirection);
    }

    public List<Vector2> GetEscapeDirections(Vector2 directionAway)
    {
        List<Vector2> directions = new List<Vector2>
        {
            directionAway, // Directly away from the player
            Vector2.Perpendicular(directionAway), // Right direction
            -Vector2.Perpendicular(directionAway) // Left direction
        };
        return directions;
    }

    public Vector2 FindBestEvadeDirection(List<Vector2> directions)
    {
        Vector2 bestDirection = directions[0];
        float furthestDistance = 0f;

        foreach (Vector2 direction in directions)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, evadeDistance, obstacleLayer);
            float distance = hit.collider == null ? evadeDistance : hit.distance;
            if (distance > furthestDistance)
            {
                furthestDistance = distance;
                bestDirection = direction;
            }
        }

        return bestDirection;
    }

    public void MoveInDirection(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Update facing direction
            if (direction.x > 0 && !facingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && facingRight)
            {
                Flip();
            }

            // Move away from the player
            rb.linearVelocity = direction.normalized * speed; // Normalize for consistent speed
        }
    }

    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}