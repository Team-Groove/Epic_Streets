using UnityEngine;

public class SecondaryAttackCooldown : MonoBehaviour
{

    [SerializeField] private AttackSystem attackSystem;
    private Animator animator;
    
    private void Awake()
    {
        attackSystem = GetComponentInParent<AttackSystem>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if (attackSystem.secondaryAttackcooldownTimer <= 0.2f && attackSystem.secondaryAttackcooldownTimer != 0)
        {
            animator.SetBool("Cooldown", true);
        }
    }
    
    public void CooldownOff()
    {
        animator.SetBool("Cooldown", false);
    }

}
