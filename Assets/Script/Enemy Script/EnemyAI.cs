using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private float time = 0f;

    [Header("Combat Settings")]
    public float maxHealth = 100f;
    private float currentHealth;

    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public float detectionRadius = 15f;

    void Start()
    {
        currentHealth = maxHealth;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null || currentHealth <= 0) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRadius)
        {
            agent.SetDestination(player.position);
        }

        time += Time.deltaTime;

        if (distance <= attackRange)
        {
            if (time >= attackCooldown && !isAttacking)
            {
                isAttacking = true;
                agent.isStopped = true;
                animator.SetTrigger("Attack");
                animator.SetBool("Attack 0", true);
                time = 0f;
            }
        }
        else
        {
            agent.isStopped = false;
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        animator.ResetTrigger("Attack");
        animator.SetBool("Attack 0", false);
        agent.isStopped = false;
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameManager.Instance.UnregisterEnemy(gameObject);
        // Optional: play death animation here before destroying
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
