using UnityEngine;

public class MaxHPIncrease : MonoBehaviour
{
    public int maxHealthIncrease = 20;
    public Health health;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponentInChildren<Health>();

        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFXClip(10);
            health.maxHealth += maxHealthIncrease;
            health.health = health.maxHealth;
            Destroy(gameObject);
        }
    }
}
