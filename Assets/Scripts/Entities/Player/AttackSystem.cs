using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem: MonoBehaviour
{

    #region VARIABLES

    public float secondaryAttackCooldown;
    private float secondaryAttackcooldownTimer;
    
    public bool normalAttack;
    public bool strongAttack;

    public bool canStrongAttack;
    public bool isAttacking;

    private PlayerInput input;
    private DistanceAttackSystem distance;
   

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        distance = GetComponent<DistanceAttackSystem>();
    }

    private void Start()
    {
        secondaryAttackcooldownTimer = secondaryAttackCooldown;
        canStrongAttack = true;
    }

    private void Update()
    {
        SecondaryAttackCooldown();
        CheckIfAttacking();
    }

    #endregion

    #region FUNCTIONS

    private void CheckIfAttacking()
    {
        if (normalAttack || strongAttack || distance.distanceAttack)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }


    #endregion

    #region INPUT_CHECK

    public void NormalAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && !strongAttack && !distance.distanceAttack)
        {
            normalAttack = true;
        }
        else
        {
            normalAttack = false;
        }
    }
    public void StrongAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed && canStrongAttack)
        {
            strongAttack = true;
            canStrongAttack = false;
        }
        else
        {
            strongAttack = false;
        }
    }
    private void SecondaryAttackCooldown()
    {
        if (secondaryAttackcooldownTimer > 0 && !canStrongAttack || !distance.canShoot)
        {
            secondaryAttackcooldownTimer -= Time.deltaTime;
            canStrongAttack = false;
            distance.canShoot = false;
        }
        
        if (secondaryAttackcooldownTimer < 0)
        {
            canStrongAttack = true;
            distance.canShoot = true;
            secondaryAttackcooldownTimer = secondaryAttackCooldown;
            return;
        }
    }
    #endregion
}