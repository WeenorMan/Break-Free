using UnityEngine;
using UnityEngine.InputSystem;

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

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction sprintAction;

    public Vector2 moveInput { get; private set; }
    public Vector2 lookInput { get; private set; }
    public bool jumpTriggered { get; private set; }
    public bool jumpHeld { get; private set; }
    public bool jumpReleased { get; private set; }
    public float sprintValue { get; private set; }


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
            Destroy(gameObject);
            print("object destroyed");
        }

        moveAction = playerControls.FindActionMap(actionMapName).FindAction(move);
        lookAction = playerControls.FindActionMap(actionMapName).FindAction(look);
        jumpAction = playerControls.FindActionMap(actionMapName).FindAction(jump);
        sprintAction = playerControls.FindActionMap(actionMapName).FindAction(sprint);
        RegisterInputActions();
    }

    void RegisterInputActions()
    {
        moveAction.performed += context => moveInput = context.ReadValue<Vector2>();
        moveAction.canceled += context => moveInput = Vector2.zero;

        lookAction.performed += context => lookInput = context.ReadValue<Vector2>();
        lookAction.canceled += context => lookInput = Vector2.zero;

        // Track whether the jump button is currently held via performed/canceled callbacks.
        // Single-frame events (pressed/released) are provided by polling WasPerformedThisFrame/WasReleasedThisFrame in Update().
        if (jumpAction != null)
        {
            jumpAction.performed += context => jumpHeld = true;
            jumpAction.canceled += context => jumpHeld = false;
        }

        sprintAction.performed += context => sprintValue = context.ReadValue<float>();
        sprintAction.canceled += context => sprintValue = 0f;
    }

    private void Update()
    {
        // Poll the action each frame so jumpReleased/jumpTriggered are single-frame flags
        if (jumpAction != null)
        {
            // true only on the frame the action was performed/released
            jumpReleased = jumpAction.WasReleasedThisFrame();
            jumpTriggered = jumpAction.WasPerformedThisFrame();
        }
        else
        {
            jumpReleased = false;
            jumpTriggered = false;
        }
    }
    private void OnEnable()
    {
        moveAction.Enable();
        lookAction.Enable();
        jumpAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        lookAction.Disable();
        jumpAction.Disable();
        sprintAction.Disable();
    }
}
