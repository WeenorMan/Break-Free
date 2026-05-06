using UnityEngine;


[CreateAssetMenu(menuName = "Enemy/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement")]
    public float patrolSpeed = 5f;

    [Header("Patrol")]
    public float groundCheckDistance = 0.7f;
    public LayerMask groundLayer;
}
