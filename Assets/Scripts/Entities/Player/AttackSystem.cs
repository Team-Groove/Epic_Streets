using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem: MonoBehaviour
{

    #region VARIABLES

    public bool normalAttack;
    public bool strongAttack;
  
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

    private void Update()
    {
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
        if (context.performed && !normalAttack && !distance.distanceAttack)
        {
            strongAttack = true;
        }
        else
        {
            strongAttack = false;
        }
    }

    #endregion
}