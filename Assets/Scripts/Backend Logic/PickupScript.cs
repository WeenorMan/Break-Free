using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public Health health;
    public int healthIncrease = 30;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health health = other.GetComponentInChildren<Health>();

        if (other.CompareTag("Player"))
        {
            health.ChangeHealth(healthIncrease);
            Destroy(gameObject);
        }
    }
}
