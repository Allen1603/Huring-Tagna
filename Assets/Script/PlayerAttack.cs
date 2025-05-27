using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
   // public Collider swordCollider; // Assign in Inspector
    private Animator animator;
    private PlayerInputAction inputActions;

    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
       // swordCollider.enabled = false;

        inputActions = new PlayerInputAction();
        inputActions.Player.Attack.performed += ctx => TryAttack();
    }

    void OnEnable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Enable();
        }
    }


    void OnDisable()
    {
        if (inputActions != null)
        {
            inputActions.Player.Disable();
        }
          
    }
    private void Update()
    {
        
    }
    void TryAttack()
    {
        if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
            lastAttackTime = Time.time;
        }
    }
    public void EndAttack()
    {
        isAttacking = false;
    }

    // Animation Event
    /*    public void EnableSwordCollider()
        {
            swordCollider.enabled = true;
        }

        // Animation Event
        public void DisableSwordCollider()
        {
            swordCollider.enabled = false;
            isAttacking = false;
            animator.SetBool("isAttacking", false);
        }*/
}
