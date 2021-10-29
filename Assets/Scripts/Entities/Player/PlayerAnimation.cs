using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{

    #region VARIABLES

    //COMPONENTES
    private Animator animator;
    private PlayerMovement movement;
    private AttackSystem attack;
    private Dash dash;
    private DistanceAttackSystem distance;
    private ComboSystemManager comboSystem;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject attackTag;

    //STRING DE ANIMACIONES
    [SerializeField] public string[] normalAttackAnimationsNames;
    

    //PUBLIC
    public int currentAttackIndex;
    public bool canMove;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        comboSystem = FindObjectOfType<ComboSystemManager>();
        GetStringFromComboSystem();
    }

    private void Start()
    {
        GetComponents();
        canMove = true;
    }

    private void Update()
    {

        GetStringFromComboSystem();

        SetAnimationBool(movement.isMoving, "Run");
        
        PlayAnimation(dash.isDashing, "Dash");
        CheckStateInfo_Play("Punch_1", attack.strongAttack, "StrongPunch", "Punch_2", "Kick_2");
        CheckStateInfo_Play("StrongPunch", attack.normalAttack, normalAttackAnimationsNames[currentAttackIndex], "DistanceAttack");
        CheckStateInfo_Play("Punch_1", distance.distanceAttack, "DistanceAttack", "Punch_2", "Kick_2", "StrongPunch");

        StopVelocityMovementWhenAttack();
    }
    
    #endregion

    #region PRIVATE_FUNCTIONS

    private void SetAnimationBool(bool isActive,string stateName)
    {
        animator.SetBool(stateName, isActive);
    }
    private void PlayAnimation(bool condition, string stateName)
    {
        if (condition)
        {
            animator.Play(stateName);
        }
    }
    private void CheckStateInfo_Play(string stateName, bool condition, string animationName, 
        string stateName_2 = default(string), string stateName_3 = default(string), string stateName_4 = default(string))
    { 
        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) &&
                    !animator.GetCurrentAnimatorStateInfo(0).IsName(stateName_2) &&
                    !animator.GetCurrentAnimatorStateInfo(0).IsName(stateName_3) &&
                    !animator.GetCurrentAnimatorStateInfo(0).IsName(stateName_4))
        {
            PlayAnimation(condition, animationName);
        }

    }
    private void GetComponents()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        dash = GetComponent<Dash>();
        attack = GetComponent<AttackSystem>();
        distance = GetComponent<DistanceAttackSystem>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    //HARDCODEADO
    private void StopVelocityMovementWhenAttack()
    {
        if (GetCurrentAnimatorState("Punch_1") ||
            GetCurrentAnimatorState("Punch_2") ||
            GetCurrentAnimatorState("Kick_2") ||
            GetCurrentAnimatorState("StrongKick") ||
            GetCurrentAnimatorState("DistanceAttack") ||
            GetCurrentAnimatorState("StrongPunch"))
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }
    }

    private void GetStringFromComboSystem()
    {
        if (comboSystem.comboStrings != null)
        {
            for (int k = 0; k < comboSystem.comboStrings.Length; k++)
            {
                normalAttackAnimationsNames[k] = comboSystem.comboStrings[k];
            }
        }
    }

    private bool GetCurrentAnimatorState(string stateName)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public IEnumerator DamageRedFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    #endregion

    #region ANIMATION_EVENTS

    public void NextAttackIndex()
    {
        switch (currentAttackIndex)
        {
            case 0:

                currentAttackIndex = 1;
                break;

            case 1:

                currentAttackIndex = 2;
                break;

            case 2:

                currentAttackIndex = 0;
                break;
        }
    }
    public void ResetAttackIndex()
    {
        currentAttackIndex = 0;
    }
    public void ResetDistanceAttack()
    {
        distance.distanceAttack = false;
    }

    //TAGS DE ATAQUE

    public void ChangeToPunch1Tag()
    {
        attackTag.tag = "Punch_1";
    }
    public void ChangeToPunch2Tag()
    {
        attackTag.gameObject.tag = "Punch_2";
    }
    public void ChangeToKick1Tag()
    {
        attackTag.gameObject.tag = "Kick_1";
    }
    public void ChangeToKick2Tag()
    {
        attackTag.gameObject.tag = "Kick_2";
    }
    public void ChangeToFinal1Tag()
    {
        attackTag.gameObject.tag = "Final_1";
    }


    #endregion

}
