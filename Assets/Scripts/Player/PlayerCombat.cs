using Player;
using UnityEngine;

namespace Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [Header("Attack Settings")]
        public int damage;
        public float attackRadius = 0.5f;
        public float attackCooldown = 1.5f;
        public Transform attackPoint;
        public LayerMask enemyLayer;

        public PlayerScript player;

        public bool canAttack => Time.time >= nextAttackTime;
        private float nextAttackTime;

        private void Start()
        {
            player = GetComponentInParent<PlayerScript>();
        }

        public void AttackAnimationFinished()
        {
            player.AttackAnimationFinished();
        }

        public void Attack()
        {
            if (!canAttack)
            {
                return;
            }

            nextAttackTime = Time.time + attackCooldown;

            Collider2D enemy = Physics2D.OverlapCircle(attackPoint.position, attackRadius, enemyLayer);
            if (enemy != null)
            {
                //AudioManager.instance.PlaySFXClip(0);
                enemy.gameObject.GetComponent<Health>().ChangeEnemyHealth(-damage, transform.position);
            }
            
        }

        

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }

        
    }

}
