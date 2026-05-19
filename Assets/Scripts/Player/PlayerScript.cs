using TMPro;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {
        #region Core Components
        [Header("Core Components")]
        public PlayerInputHandler inputHandler;
        public StateMachine sm;
        public Animator anim;
        public Rigidbody2D rb;
        public PlayerCombat combat;
        public Health health;
        public PlayerDamage damage;
        public HazardScript hazard;
        #endregion Core Components

        public TextMeshProUGUI healthTotalText;


        [Header("Wall/Ground Check Settings")]
        [SerializeField] private Transform groundCheckPos;
        [SerializeField] private Vector2 groundCheckSize = new Vector2 (0.49f, 0.03f);
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform wallCheckPos;
        [SerializeField] private Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
        [SerializeField] private LayerMask wallLayer;
        [SerializeField] public Transform currentRespawn;

        #region Player Settings
        [Header("Movement Settings")]
        public float walkSpeed = 3f;
        public int facingDirection = 1;
        
        [Header("Jump Settings")]
        public float lowJumpMultiplier = 2f;
        public float jumpForce = 5f;
        public float fallMultiplier = 2f;
        public float coyoteTime = 0.2f;
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
        public bool isGrounded;
        public bool isFacingRight;
        public bool isControlLocked;
        public bool hasJumped;
        public bool isDying = false;
       

        #endregion Player Settings

        #region States
        public IdleState idleState;
        public JumpState jumpState;
        public WalkState walkState;
        public FallState fallState;
        public DashState dashState;
        public WallJumpState wallJumpState;
        public WallSlideState wallSlideState;
        public AttackState attackState;
        public DamagedState damagedState;
        public PlayerDeathState playerDeathState;
        #endregion States

        private void Awake()
        {
            ServiceLocator.Register<PlayerScript>(this);

            sm = gameObject.AddComponent<StateMachine>();

            if (PlayerInputHandler.Instance != null)
            {
                inputHandler = PlayerInputHandler.Instance;
            }
            else
            {
                GameObject inputHost = new GameObject("PlayerInputHandler");
                inputHandler = inputHost.AddComponent<PlayerInputHandler>();
            }
            anim = transform.GetChild(0).GetComponent<Animator>();
           
            rb = GetComponent<Rigidbody2D>();

            // add new states here
            idleState = new IdleState(this, sm);
            jumpState = new JumpState(this, sm);
            walkState = new WalkState(this, sm);
            fallState = new FallState(this, sm);
            dashState = new DashState(this, sm);
            wallJumpState = new WallJumpState(this, sm);
            wallSlideState = new WallSlideState(this, sm);
            attackState = new AttackState(this, sm);
            damagedState = new DamagedState(this, sm);
            playerDeathState = new PlayerDeathState(this, sm);

            sm.Init(idleState);
        }

        private void Start()
        {
            //rb.gravityScale = normalGravity;
            //isGrounded = true;
            isFacingRight = true;
        }

        private void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
            if (rb == null) return;
        }
        void Update()
        {
            healthTotalText.text = "Health - " + health.health + "/" + health.maxHealth;

            if (GetIsGrounded())
            {
                hasDashed = false;
                hasJumped = false;
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (inputHandler.jumpTriggered)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            sm.CurrentState.LogicUpdate();

            Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.red);

        }

        

        public bool GetIsGrounded()
        {
            if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
            {
                return true;
            }
            else
            {
                inAir = true;
                return false;
            }
        }

        public bool GetIsOnWall()
        {

            if(Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0, wallLayer))
            {
                Debug.Log("wall touched");

                return true;
            }
            else
            {
                Debug.Log("not touch");

                return false;
            }
        }

        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(groundCheckPos.position,groundCheckSize);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);

        }

        public void CheckForDash()
        {
            if (isControlLocked)
            {
                return;
            }

            if ((GetIsGrounded() || !hasDashed) && inputHandler.dashTriggered && canDash)
            {
                sm.ChangeState(dashState);
            }
        }

        public void CheckForJump()
        {
            if (isControlLocked)
            {
                return;
            }

            if (coyoteTimeCounter > 0 && jumpBufferCounter > 0 && !hasJumped)
            {
                //Debug.Log("coyote time jump = " + coyoteTimeCounter);

                coyoteTimeCounter = 0;
                jumpBufferCounter = 0;

                sm.ChangeState(jumpState);
            }
        }

        public void CheckForFall()
        {
            if (isControlLocked)
            {
                return;
            }

            if (rb.linearVelocity.y <= 0f && !GetIsGrounded())
            {
                sm.ChangeState(fallState);
            }
        }

        public void CheckForWalk()
        {
            if (isControlLocked)
            {
                rb.linearVelocity = Vector2.zero;
                return;
            }

            if (inputHandler.moveInput != Vector2.zero)
            {
                sm.ChangeState(walkState);
            }

        }

        public void CheckForAttack()
        {
            if (isControlLocked)
            {
                return;
            }


            if (inputHandler.attackTriggered && combat.canAttack)
            {
                sm.ChangeState(attackState);
            }
        }

        public void AttackAnimationFinished()
        {
            sm.CurrentState.AttackAnimationFinished();
        }

        public void Flip()
        {
            if (isControlLocked)
            {
                return;
            }

            if (inputHandler.moveInput.x > 0.1f)
            {
                facingDirection = 1;
            }
            else if (inputHandler.moveInput.x < -0.1f)
            {
                facingDirection = -1;
            }

            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }

        public void ForceFlip()
        {
            Vector3 localScale = transform.localScale;
            isFacingRight = !isFacingRight;

            localScale.x *= -1f;

            transform.localScale = localScale;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Respawn"))
            {
                currentRespawn = other.gameObject.transform;
            }
        }

    }

}