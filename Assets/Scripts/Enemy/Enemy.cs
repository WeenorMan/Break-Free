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
    public EnemyCombat Combat { get; private set; }
    #endregion components

    //Persistence 
    private WorldState worldState;
    private PersistentGuid guid;
    private bool isDefeated;

    private void Awake()
    {
        guid = GetComponent<PersistentGuid>();
        RB = GetComponent<Rigidbody2D>();
        ESM = new EStateMachine();
        Senses = GetComponent<EnemySenses>();
        Animator = GetComponent<Animator>();
        Combat = GetComponent<EnemyCombat>();
    }

    public void Start()
    {
        //Persistence
        worldState = ServiceLocator.Get<WorldState>();
        if (worldState.defeatedEnemies.Contains(guid.Guid))
        {
            Destroy(gameObject);
            return;
        }

        ESM.Init(new PatrolState(this, ESM));
    }

    private void Update() => ESM.CurrentState?.LogicUpdate();
    private void FixedUpdate() => ESM.CurrentState?.PhysicsUpdate();
    public void OnAnimationFinished() => ESM.CurrentState?.OnAnimationFinished();

    public void Flip()
    {
        FacingDirection *= -1;

        Vector3 scale = transform.localScale;
        scale.x = FacingDirection;
        transform.localScale = scale;
    }

    public void FaceTarget(Transform target)
    {
        float offset = target.position.x - transform.position.x;
        int direction = offset > 0 ? 1 : -1;

        if(direction != FacingDirection)
        {
            Flip();
        }
    }

    /*public void Die()
    {
        if (isDefeated)
        {
            return;
        }

        worldState.defeatedEnemies.Add(guid.Guid);
    }*/

}
