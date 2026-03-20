using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

namespace Player
{

    public class PlayerScript : MonoBehaviour
    {

        public Rigidbody2D rb;
        [SerializeField] public float jumpForce = 5f;
        [SerializeField] public float walkSpeed = 3f;
        [SerializeField] private float fallMultiplier = 2f;

        public StateMachine sm;

        // variables holding the different player states
        public IdleState idleState;
        public JumpState jumpState;


        public bool isGrounded;
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

            sm.Init(idleState);
        }

        private void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
        }
        void Update()
        {
            sm.CurrentState.LogicUpdate();

            Debug.DrawRay(transform.position, Vector2.down * 0.75f, Color.red);
            //CheckForWalk();
            //CheckForJump();
        }
        public bool GetIsGrounded()
        {
            return Physics2D.Raycast(transform.position, Vector2.down, 0.75f, LayerMask.GetMask("Ground"));
        }

    }

}