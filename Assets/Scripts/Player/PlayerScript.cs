using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {

        public StateMachine sm;
        public Rigidbody2D rb;
        public bool isGrounded;
        public bool isFacingRight;

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

        

        // variables holding the different player states
        public IdleState idleState;
        public JumpState jumpState;
        public WalkState walkState;
        public FallState fallState;
        public GroundDashState groundDashState;
        public AirDashState airDashState;

        private void Awake()
        {
           
        }

        private void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            rb = GetComponent<Rigidbody2D>();
            isGrounded = true;

            // add new states here
            idleState = new IdleState(this, sm);
            jumpState = new JumpState(this, sm);
            walkState = new WalkState(this, sm);
            fallState = new FallState(this, sm);
            groundDashState = new GroundDashState(this, sm);
            airDashState = new AirDashState(this, sm);

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
            sm.CurrentState.LogicUpdate();

            Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.red);

            stateText.text = "State: " + sm.CurrentState;
            Flip();
        }
        public bool GetIsGrounded()
        {
            if(Physics2D.BoxCast(transform.position, boxSize,0, -transform.up, castDistance, LayerMask.GetMask("Ground")))
            {
                return true;
            }
            else
            {
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
            if (PlayerInputHandler.Instance.dashTriggered )
            {
                if( GetIsGrounded() )
                {
                    sm.ChangeState(groundDashState);
                }
                else
                {
                    sm.ChangeState(airDashState);
                }
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