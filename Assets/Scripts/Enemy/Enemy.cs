using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region variables
    public int FacingDirection { get; private set; } = 1;


    #endregion variables

    #region components
    public EnemyConfig Config;
    public EnemySenses Senses { get; private set; }
    public Rigidbody2D RB { get; private set; }
    public EStateMachine ESM { get; private set; }
    public Animator Animator { get; private set; }
    #endregion components

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        ESM = new EStateMachine();
        Senses = GetComponent<EnemySenses>();
        Animator = GetComponent<Animator>();
    }

    public void Start()
    {
        ESM.Init(new PatrolState(this, ESM));
    }

    private void Update() => ESM.CurrentState?.LogicUpdate();
    private void FixedUpdate() => ESM.CurrentState?.PhysicsUpdate();

    public void Flip()
    {
        FacingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x = FacingDirection;
        transform.localScale = scale;
    }



}
