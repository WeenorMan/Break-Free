using Player;
using System.Collections;
using UnityEngine;

public class HazardScript : MonoBehaviour
{
    [SerializeField] public Transform respawnPoint1;
    [SerializeField] public Transform respawnPoint2;
    public int damage;
    public float damageCooldown = 1f;
    public LayerMask playerLayer;
    public Health health;


    public void DealDamage(Collider2D other)
    {
        Health health = other.GetComponentInChildren<Health>();

        health.ChangeHealth(-damage);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
       if(other.CompareTag("Player"))
       {
            DealDamage(other);
            StartCoroutine(Respawn(other.gameObject));

            Debug.Log("damaged the player");
       } 
    }

    private IEnumerator Respawn(GameObject player)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();

        if (playerScript != null)
        {
            playerScript.isControlLocked = true;

            playerScript.transform.position = playerScript.currentRespawn.position;
        }

        playerScript.isControlLocked = false;


        yield break;
    }
}
