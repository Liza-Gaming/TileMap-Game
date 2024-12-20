using UnityEngine;
using UnityEngine.InputSystem;
public class BoatMovement : MonoBehaviour
{
    //    public float speed = 5f;

    //    void Update()
    //    {
    //        float horizontal = Input.GetAxis("Horizontal");
    //        float vertical = Input.GetAxis("Vertical");

    //        Vector3 movement = new Vector3(horizontal, vertical, 0f);
    //        transform.position += movement * speed * Time.deltaTime;
    //    }
    //}

    [SerializeField]
    [Tooltip("Control the player with defined keyboard buttons")]
    public InputAction PlayerControls;


    [SerializeField]
    [Tooltip("Movement speed in meters per second")]
    private float _speed = 3f;

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