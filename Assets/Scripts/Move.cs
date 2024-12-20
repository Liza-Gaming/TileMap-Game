using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 *  I learned about 2D animation changing from here: https://www.youtube.com/watch?v=hkaysu1Z-N8&t=147s&ab_channel=Brackeys
 *  I learned about moving objects from here: https://www.youtube.com/watch?v=WxCsnNiJnhA&ab_channel=PPHGames
 *  To implement the player movements with the new input system (Unity 6) I watched this: https://www.youtube.com/watch?v=HmXU4dZbaMw&t=89s&ab_channel=SpudMasterStudios
 *  Minimaps: 
 *  https://www.youtube.com/watch?v=kWhOMJMihC0&t=274s&ab_channel=CodeMonkey
 *  https://www.youtube.com/watch?v=TkegkmRbrN0&t=640s&ab_channel=MuddyWolf
 */
public class Move : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Control the player with defined keyboard buttons")]
    public InputAction PlayerControls;

    public Animator animator;

    [SerializeField]
    [Tooltip("Movement speed in meters per second")]
    private float _speed = 5f;

    private Rigidbody2D rb;

    private bool facingRight = true; // Tells the direction that the player is facing

    Vector2 moveDir = Vector2.zero; // Direction of the player by vector

    void OnEnable()
    {
        PlayerControls.Enable();
    }

    void OnDisable()
    {
        PlayerControls.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = PlayerControls.ReadValue<Vector2>();
    }

    // Moves the player in every frame when the user is clicking on the buttons
    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveDir.x * _speed, moveDir.y * _speed); //Move corresponding to the vector and speed

        // If the input is moving the player left and the player is facing right.
        if (moveDir.x < 0 && facingRight)
        {
            Flip();
        }
        // Otherwise if the input is moving the player right and the player is facing left.
        else if (moveDir.x > 0 && !facingRight)
        {
            Flip();
        }
        // Change the animation state to "walk"
        animator.SetFloat("horizontal", Mathf.Abs(rb.linearVelocity.magnitude));
        animator.SetFloat("vertical", Mathf.Abs(rb.linearVelocity.magnitude));
    }

    // Flips the player so the face of the player will be in the appropriate direction
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