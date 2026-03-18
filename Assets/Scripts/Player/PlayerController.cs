using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float walkSpeed = 3f;
    [SerializeField] private float fallMultiplier = 2f;

    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isGrounded = true;
    }

    private void FixedUpdate()
    {
        if (!GetIsGrounded())
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.fixedDeltaTime;

            if (GetIsGrounded())
            {
                Debug.Log("WHEYYYY");
                rb.linearVelocity += Vector2.up;
            }

            //if (GetIsGrounded())
            //{
            //    isGrounded = true;
            //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            //    Debug.Log(isGrounded);
            //}
        }
       
    }

    void Update()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.red);

        if (PlayerInputHandler.Instance.JumpTriggered && GetIsGrounded())
        {
            Debug.Log("jump pressed");
            Jump();
            isGrounded = false;
        } 

    }

    private bool GetIsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    } 

    void CheckForLanding()
    {
        if(rb.linearVelocity.y < 0.1f)
        {

        }
    }
}
