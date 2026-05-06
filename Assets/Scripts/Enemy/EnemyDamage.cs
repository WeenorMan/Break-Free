using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public Health health;

    private void OnEnable()
    {
        health.OnDamaged += HandleDamage;
    }
    private void OnDisable()
    {
        health.OnDamaged -= HandleDamage;
    }

    void HandleDamage()
    {

    }
}
