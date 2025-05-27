using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    private Animator animator;
    private bool isAttacking = false;
    private float time =  0f;

    public float attackRange = 5f;
    public float attackCooldown = 1f;
    public float detectionRadius = 15f;

    void Start()
    {
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
        if (player == null) return;

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
        agent.isStopped = false;
    }

    // Optional: Visualize detection radius in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
