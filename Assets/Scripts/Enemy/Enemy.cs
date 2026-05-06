using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }
    public EStateMachine ESM { get; private set; }

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
        ESM = new EStateMachine();
    }

    public void Start()
    {
        ESM.Init(new PatrolState(this, ESM));
    }

    private void Update() => ESM.CurrentState?.LogicUpdate();
    private void FixedUpdate() => ESM.CurrentState?.PhysicsUpdate();





}
