using UnityEngine;

public class DamagedState : EState
{
    private float knockbackVelocity;
    private float knockbackDuration;

    public DamagedState(Enemy enemy, int knockbackDir, EStateMachine esm) : base(enemy, esm)
    {
        knockbackVelocity = knockbackDir * this.config.knockbackForce;
    }

    public override void Enter()
    {
        base.Enter();
        anim.Play("Hurt");
        AudioManager.instance.PlaySFXClip(0);
        knockbackDuration = config.knockbackDuration;
        rb.linearVelocity = new Vector2(knockbackVelocity,rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        knockbackDuration -= Time.fixedDeltaTime;
        if(knockbackDuration <= 0)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            if (!senses.IsAtCliff())
            {
                esm.ChangeState(new EnIdleState(enemy, esm));
            }
        }
    }
}
