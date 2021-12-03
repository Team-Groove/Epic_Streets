using System;
using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{

    #region VARIABLES

    //COMPONENTES
    private Animator animator;
    private PlayerMovement movement;
    private AttackSystem attack;
    private PlayerController controller;
    private Dash dash;
    private DistanceAttackSystem distance;
    private ComboSystemManager comboSystem;
    public AttackDamageManager damageManager;
    private AudioManager audioManager;

    public SpriteRenderer spriteRenderer;

    [SerializeField] private Material glowMaterial;
    [SerializeField] private GameObject attackTag;

    [SerializeField] public EffectColor[] attackColors;

    //STRING DE ANIMACIONES
    [SerializeField] public string[] normalAttackAnimationsNames;


    //PUBLIC
    public int currentAttackIndex;
    public bool canMove;
    public bool isPlayingSound;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        comboSystem = FindObjectOfType<ComboSystemManager>();
        damageManager = FindObjectOfType<AttackDamageManager>();
        audioManager = FindObjectOfType<AudioManager>();
        GetStringFromComboSystem();
    }

    private void Start()
    {
        GetComponents();
        canMove = true;
    }

    private void Update()
    {
        if (!controller.IsDead && !controller.InDialogue())
        {
            GetStringFromComboSystem();

            SetAnimationBool(movement.isMoving, "Run");

            PlayAnimation(dash.isDashingX, "Dash");
            PlayAnimation(dash.isDashingY, "Dash_Y");
            //CONDICION ATAQUE FUERTE
            CheckStateInfo_Play("Punch_1", attack.strongAttack, "StrongPunch", "Punch_2", "Kick_2");
            //CONDICION ATAQUES NORMALES
            CheckStateInfo_Play("StrongPunch", attack.normalAttack, normalAttackAnimationsNames[currentAttackIndex], "DistanceAttack");
            //CONDICION ATAQUE A DISTANCIA
            CheckStateInfo_Play("Punch_1", distance.distanceAttack, "DistanceAttack", "Punch_2", "Kick_2", "StrongPunch");

            StopVelocityMovementWhenAttack();

           
        }
       
        DeathAnimation();
        

        //CAMBIAR EL COLOR DEL MATERIAL 

        SetEffectColor("Punch_1", "Veneno");
        SetEffectColor("Punch_2", "Hielo");
        SetEffectColor("Kick_1", "Viento");
        SetEffectColor("Kick_2", "Fuego");
        SetEffectColor("StrongPunch", "Final_1");

        //EFECTOS DISTINTOS SEGUN EL SLOT
        SpecialEffectAttacks();

        //LETRAS AMARILLAS DE GOLPE CRITICO
        if (currentAttackIndex == 2)
        {
            damageManager.criticalHit = true;
        }
        else
        {
            damageManager.criticalHit = false;
        }
    }

    #endregion

    #region PRIVATE_FUNCTIONS

    private void SetEffectColor(string animationName, string colorName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName))
        {
            for (int i = 0; i < attackColors.Length; i++)
            {
                if (attackColors[i].name == colorName)
                {
                    glowMaterial.color = attackColors[i].color * attackColors[i].intensity;
                }
            }
        }
    }
    private void SetAnimationBool(bool isActive, string stateName)
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
        controller = GetComponent<PlayerController>();
    }

    //HARDCODEADO
    private void StopVelocityMovementWhenAttack()
    {
        if (GetCurrentAnimatorState("Punch_1") ||
            GetCurrentAnimatorState("Punch_2") ||
            GetCurrentAnimatorState("Kick_1") ||
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
                try
                {
                    normalAttackAnimationsNames[k] = comboSystem.comboStrings[k];
                }
                catch (IndexOutOfRangeException e)
                {
                    Debug.Log("PlayerAnimation.'Normal Attack Animation Names' probablemente vacío.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
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

    private void DeathAnimation()
    {
      
        animator.SetBool("isDead", controller.IsDead);
        
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
    public void ResetStrongAttack()
    {
        attack.strongAttack = false;
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


    //EVENTOS DE ANIMACION ESPECIALES SEGUN EL SLOT

    private void ChangeSpeed(int index, string attackName, string animationName, float animationSpeed)
    {
        if (normalAttackAnimationsNames[index] == attackName)
        {
            animator.SetFloat(animationName, animationSpeed);
        }
    }

    private void SetPoisonAndDamage(int index, string attackName, bool tf, int newDamage)
    {
        if (normalAttackAnimationsNames[index] == attackName)
        {
            damageManager.punch1 = newDamage;
            damageManager.canPoison = tf;
        }
    }
    private void SetBurnAndDamage(int index, string attackName, bool tf, int newDamage)
    {
        if (normalAttackAnimationsNames[index] == attackName)
        {
            damageManager.kick2 = newDamage;
            damageManager.canBurn = tf;
        }
    }
    private void SetFreezeAndDamage(int index, string attackName, bool tf, int newDamage)
    {
        if (normalAttackAnimationsNames[index] == attackName)
        {
            damageManager.punch2 = newDamage;
            damageManager.canFreeze = tf;
        }
    }
    private void SetPushbackAndDamage(int index, string attackName, bool tf, int newDamage)
    {
        if (normalAttackAnimationsNames[index] == attackName)
        {
            damageManager.kick1 = newDamage;
            damageManager.canWindBack = tf;
        }
    }

    private void SpecialEffectAttacks()
    {
        ChangeSpeed(2, "Punch_1", "Punch1Speed", 0.6f);
        ChangeSpeed(2, "Punch_2", "Punch2Speed", 0.6f);
        ChangeSpeed(2, "Kick_1", "Kick1Speed", 0.6f);
        ChangeSpeed(2, "Kick_2", "Kick2Speed", 0.7f);

        ChangeSpeed(1, "Punch_1", "Punch1Speed", 1.4f);
        ChangeSpeed(1, "Punch_2", "Punch2Speed", 1.3f);
        ChangeSpeed(1, "Kick_1", "Kick1Speed", 1.2f);
        ChangeSpeed(1, "Kick_2", "Kick2Speed", 1.1f);
        
        ChangeSpeed(0, "Punch_1", "Punch1Speed", 1f);
        ChangeSpeed(0, "Punch_2", "Punch2Speed", 1f);
        ChangeSpeed(0, "Kick_1", "Kick1Speed", 1f);
        ChangeSpeed(0, "Kick_2", "Kick2Speed", 1f);


        SetPoisonAndDamage(2, "Punch_1", true, 18);
        SetFreezeAndDamage(2, "Punch_2", true, 20);
        SetPushbackAndDamage(2, "Kick_1", true, 23);
        SetBurnAndDamage(2, "Kick_2", true, 25);
        
        SetPoisonAndDamage(1, "Punch_1", false, 13);
        SetFreezeAndDamage(1, "Punch_2", false, 15);
        SetPushbackAndDamage(1, "Kick_1", false, 19);
        SetBurnAndDamage(1, "Kick_2", false, 20);

        SetPoisonAndDamage(0, "Punch_1", false, 9);
        SetFreezeAndDamage(0, "Punch_2", false, 13);
        SetPushbackAndDamage(0, "Kick_1", false, 17);
        SetBurnAndDamage(0, "Kick_2", false, 19);
     
    }

    //SOUNDS EVENTS

    public void PlaySoundDash()
    {
        audioManager.Play("Dash");
    }
    public void PlaySoundFireKick()
    {
        audioManager.Play("FireKick");
    }
    public void PlaySoundPoisonFist()
    {
        audioManager.Play("PoisonFist");
    }
    public void PlaySoundIceFist()
    {
        audioManager.Play("IceFist");
    }
    public void PlaySoundWindKick()
    {
        audioManager.Play("WindKick");
    }
    #endregion

}
