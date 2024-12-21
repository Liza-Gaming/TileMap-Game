using UnityEngine;

/**
 * This component chases a given target object.
 */
public class Chaser : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    public Transform player;

    private bool facingRight = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public Vector3 TargetObjectPosition()
    {
        return player.position;
    }

    private void Update()
    {
        Vector2 directionAway = (player.position - transform.position).normalized;
        rb.linearVelocity = directionAway * speed;

        if (directionAway.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (directionAway.x < 0 && facingRight)
        {
            Flip();
        }
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