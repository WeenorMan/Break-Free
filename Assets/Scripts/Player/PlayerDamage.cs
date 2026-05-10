using Player;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    public Health health;

    [Header("KnockBack Settings")]
    public float knockbackForce = 20;
    public float knockbackDuration = 0.2f;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
        health.OnDeath += HandleDeath;
    }
    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
        health.OnDeath -= HandleDeath;
    }

    void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        player.damagedState.SetParameters(knockbackDir);
        player.sm.ChangeState(player.damagedState);
    }

    void HandleDeath()
    {
         player.sm.ChangeState(player.playerDeathState);
         Physics2D.IgnoreLayerCollision(6, 9, true);
    }
}
