using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputHandler : MonoBehaviour
    {
        [Header("Input Action Asset")]
        [SerializeField] private InputActionAsset playerControls;

        [Header("Action Map Name Reference")]
        [SerializeField] private string actionMapName = "Player";

        [Header("Action Name References")]
        [SerializeField] private string move = "Move";
        [SerializeField] private string look = "Look";
        [SerializeField] private string jump = "Jump";
        [SerializeField] private string sprint = "Sprint";
        [SerializeField] private string dash = "Dash";
        [SerializeField] private string attack = "Attack";


        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction jumpAction;
        private InputAction sprintAction;
        private InputAction dashAction;
        private InputAction attackAction;

        public Vector2 moveInput { get; private set; }
        public Vector2 lookInput { get; private set; }
        public bool jumpTriggered { get; private set; }
        public bool jumpHeld { get; private set; }
        public bool jumpReleased { get; private set; }
        public float sprintValue { get; private set; }
        public bool dashTriggered { get; private set; }
        public bool dashReleased { get; private set; }
        public bool attackTriggered { get; private set; }


        public static PlayerInputHandler Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                print("object created");

            }
            else
            {
                
                Destroy(this);
                print("duplicate PlayerInputHandler component removed");
                return;
            }

            moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
            lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
            jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
            sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
            dashAction = playerControls.FindActionMap(actionMapName).FindAction(dash);
            attackAction = playerControls.FindActionMap(actionMapName).FindAction(attack);
            RegisterInputActions();
        }

        void RegisterInputActions()
        {
            moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
            moveAction.canceled += context => moveInput = Vector2.zero;

            lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
            lookAction.canceled += context => lookInput = Vector2.zero;

            if (jumpAction != null)
            {
                jumpAction.performed += context => jumpHeld = true;
                jumpAction.canceled += context => jumpHeld = false;
            }

            sprintAction.performed += context => sprintValue = context.ReadValue<float>();
            sprintAction.canceled += context => sprintValue = 0f;

            //dashAction.performed += context => dashTriggered = true;
            // dashAction.canceled += context => dashTriggered = false;
        }

        private void Update()
        {
            if (dashAction != null)
            {
                dashTriggered = dashAction.WasPerformedThisFrame();
                dashReleased = dashAction.WasReleasedThisFrame();
            }
            else
            {
                dashTriggered = false;
                dashReleased = false;
            }


            if (jumpAction != null)
            {
                jumpTriggered = jumpAction.WasPerformedThisFrame();
                jumpReleased = jumpAction.WasReleasedThisFrame();
            }
            else
            {
                jumpTriggered = false;
                jumpReleased = false;
            }
        }
        private void OnEnable()
        {
            moveAction.Enable();
            lookAction.Enable();
            jumpAction.Enable();
            sprintAction.Enable();
            dashAction.Enable();
        }

        private void OnDisable()
        {
            moveAction.Disable();
            lookAction.Disable();
            jumpAction.Disable();
            sprintAction.Disable();
            dashAction.Disable();
        }
    }
    
}
