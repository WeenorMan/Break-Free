using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {
        #region Core Variables
        public StateMachine sm;
        public Animator anim;
        public Rigidbody2D rb;
        public bool isGrounded;
        public bool isFacingRight;
        #endregion Core Variables

        public TMPro.TextMeshProUGUI stateText;

        [Header("Box Cast Settings")]
        public Vector2 boxSize;
        [SerializeField] private float castDistance;

        [Header("Movement Settings")]
        [SerializeField] public float walkSpeed = 3f;
        
        [Header("Jump Settings")]
        [SerializeField] public float lowJumpMultiplier = 2f;
        [SerializeField] public float jumpForce = 5f;
        [SerializeField] public float fallMultiplier = 2f;
        [SerializeField] public float coyoteTime = 0.2f;
        [SerializeField] private float coyoteTimeCounter;
        [SerializeField] private float jumpBufferTime = 0.2f;
        [SerializeField] private float jumpBufferCounter;

        [Header("Dash Settings")]
        public float dashDuration = 0.2f;
        public float dashCooldown = 1f;
        public float dashSpeed = 10f;

        public bool isDashing;
        public bool hasDashed;
        public bool canDash = true;
        public float gravity;
        public bool inAir;

        // variables holding the different player states
        public IdleState idleState;
        public JumpState jumpState;
        public WalkState walkState;
        public FallState fallState;
        public DashState dashState;

        private void Awake()
        {
           
        }

        private void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            anim = transform.GetChild(0).GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
            isGrounded = true;

            // add new states here
            idleState = new IdleState(this, sm);
            jumpState = new JumpState(this, sm);
            walkState = new WalkState(this, sm);
            fallState = new FallState(this, sm);
            dashState = new DashState(this, sm);

            isFacingRight = true;

            sm.Init(idleState);
        }

        private void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();

            if (rb == null) return;

           
            
        }
        void Update()
        {
            if (GetIsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (PlayerInputHandler.Instance.jumpTriggered)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            sm.CurrentState.LogicUpdate();

            Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.red);

            stateText.text = "State: " + sm.CurrentState;
            Flip();
        }
        public bool GetIsGrounded()
        {
            if(Physics2D.BoxCast(transform.position, boxSize,0, -transform.up, castDistance, LayerMask.GetMask("Ground")))
            {
                inAir = false;
                return true;
            }
            else
            {
                inAir = true;
                return false;
            }
            //return Physics2D.Raycast(transform.position, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        }

        public void CheckForDash()
        {
            if (PlayerInputHandler.Instance.dashTriggered && canDash)
            {
                sm.ChangeState(dashState);
            }
        }

        public void CheckForJump()
        {
            if (coyoteTimeCounter > 0 && jumpBufferCounter > 0)
            {
                Debug.Log("coyote time jump = " + coyoteTimeCounter);

                coyoteTimeCounter = 0;
                jumpBufferCounter = 0;


                sm.ChangeState(jumpState);
            }
        }

        public void CheckForFall()
        {
            if (rb.linearVelocity.y <= 0f && !GetIsGrounded())
            {
                sm.ChangeState(fallState);
            }
        }

        public void CheckForWalk()
        {
            if (PlayerInputHandler.Instance.moveInput != Vector2.zero)
            {
                sm.ChangeState(walkState);
            }

        }

        public void Flip()
        {
            if(isFacingRight && PlayerInputHandler.Instance.moveInput.x < 0 || !isFacingRight && PlayerInputHandler.Instance.moveInput.x  > 0)
            {
                Vector3 localScale = transform.localScale;
                isFacingRight = !isFacingRight;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }

    }

}