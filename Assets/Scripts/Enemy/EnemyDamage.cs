using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    public Health health;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
    }
    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
    }

    void HandleDamage(Vector2 sourcePosition)
    {
        int knockbackDir = 0;
        knockbackDir = transform.position.x > sourcePosition.x ? 1 : -1;

        enemy.ESM.ChangeState(new DamagedState(enemy, knockbackDir, enemy.ESM));
    }
}
