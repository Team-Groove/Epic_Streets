using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyHurtBox : MonoBehaviour
{
    #region VARIABLES
    
    private EnemyController enemy;
    private AttackDamageManager damageManager;
    private EnemyMovement movement;
    private EnemyMovement sprite;
    private AudioManager audioManager;
    private StatusEffects statusEffects;

    [SerializeField] private GameObject popUpNumber;
    [SerializeField] private GameObject popUpNumberSpawnPoint;
    [SerializeField] private GameObject hitSpawnPoint;

    [SerializeField] private ParticleSystem hitEffect;

    public int staggerCount;
    public float timeBetweenStagger;
    public float staggerTimer;
    public float staggerResistence;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
        sprite = GetComponentInParent<EnemyMovement>();
        movement = GetComponentInParent<EnemyMovement>();
        audioManager = FindObjectOfType<AudioManager>();
        statusEffects = GetComponentInParent<StatusEffects>();
        staggerTimer = timeBetweenStagger;
    }

    private void Update()
    {
        DelayStaggerTimer();
       
    }

    #endregion

    #region FUNCTIONS

    private void CheckAttackTag(Collider2D collision, string nameOfAttack, int dmgReceived, string audioName, 
        float pitch, bool canStagger, bool ableToApplyStatus, bool canApplyStatus, _statusEffects newStatus)
    {
        if (collision.gameObject.CompareTag(nameOfAttack))
        {
            if (ableToApplyStatus)
            {
                CheckIfCanApplyStatus(canApplyStatus, newStatus);
            }
            

            if (canStagger && !enemy.isDummy && staggerCount != staggerResistence)
            {
                sprite.StartCoroutine("EnemyDamageRedFeedback");
                enemy.StartCoroutine("GetStunned");
                
                staggerCount++;
            }
            else
            {
                enemy.StartCoroutine("EnemyDamageRedFeedback");
            }
            enemy.ReceiveDamage(dmgReceived);
            audioManager.Play(audioName);
            popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + dmgReceived.ToString());
            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);
            Instantiate(hitEffect, new Vector3(Random.Range(hitSpawnPoint.transform.position.x + 0.5f , hitSpawnPoint.transform.position.x - 0.5f), Random.Range(hitSpawnPoint.transform.position.y + 0.5f, hitSpawnPoint.transform.position.y - 0.5f), hitSpawnPoint.transform.position.z), Quaternion.identity);

            CineMachineShake.Instance.ShakeCamera(0.3f, 0.2f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackTag(collision, "Punch_1", damageManager.punch1, "HurtBox_1", 1f, false, true, damageManager.canPoison, _statusEffects.poisoned);
        CheckAttackTag(collision, "Punch_2", damageManager.punch2, "HurtBox_3", 0.8f, true, true, damageManager.canFreeze, _statusEffects.freezed);
        CheckAttackTag(collision, "Kick_1", damageManager.kick1, "HurtBox_2", 0.6f, true, true, damageManager.canWindBack, _statusEffects.pushedBack);
        CheckAttackTag(collision, "Kick_2", damageManager.kick2, "HurtBox_4", 0.5f,true, true, damageManager.canBurn, _statusEffects.burned);
        CheckAttackTag(collision, "Final_1", damageManager.final1, "HurtBox_1", 0.4f, true, true, damageManager.canStun, _statusEffects.stunned);
        CheckAttackTag(collision, "PlayersProjectile", damageManager.longDistance, "HurtBox_1", 0.3f, false, false, false, _statusEffects.normal);
    }
 

    private void DelayStaggerTimer()
    {
        
        if (staggerCount != 0)
        {
          
            if (staggerTimer > 0)
            {
                staggerTimer -= Time.deltaTime;
            }
            else if (staggerTimer <= 0)
            {
                staggerCount = 0;
                staggerTimer = timeBetweenStagger;
                return;
            }
        }
       
    }
    private void CheckIfCanApplyStatus(bool canStatus, _statusEffects newStatus)
    {
        if (canStatus)
        {
            statusEffects.actualStatusEffect = newStatus;
        }
    }
    #endregion
}

