using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyHitBox : MonoBehaviour
{
    #region VARIABLES
    
    private EnemyController enemy;
    private AttackDamageManager damageManager;
    private EnemyMovement movement;
    private EnemyMovement sprite;

    [SerializeField] private GameObject popUpNumber;
    [SerializeField] private GameObject popUpNumberSpawnPoint;
    [SerializeField] private GameObject hitSpawnPoint;

    [SerializeField] private ParticleSystem hitEffect;

    public int staggerCount;
    public float timeBetweenStagger;
    public float staggerTimer;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
        sprite = GetComponentInParent<EnemyMovement>();
        movement = GetComponentInParent<EnemyMovement>();
        staggerTimer = timeBetweenStagger;
    }

    private void Update()
    {
        DelayStaggerTimer();
       
    }

    #endregion

    #region FUNCTIONS

    private void CheckAttackTag(Collider2D collision, string nameOfAttack, int dmgReceived, AudioClip audio, float pitch,bool canStun)
    {
        if (collision.gameObject.CompareTag(nameOfAttack))
        {
            
            if (canStun && !enemy.isDummy && staggerCount <= 1)
            {
                sprite.StartCoroutine("EnemyDamageRedFeedback");
                enemy.StartCoroutine("GetStunned");
                movement.PushEnemyBack();
                staggerCount++;
            }
            else
            {
                enemy.StartCoroutine("EnemyDamageRedFeedback");
            }
            enemy.ReceiveDamage(dmgReceived);
            SFXController.instance.PlayOnHitEnemySound(audio, 1f, pitch);
            popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + dmgReceived.ToString());
            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);
            Instantiate(hitEffect, new Vector3(Random.Range(hitSpawnPoint.transform.position.x + 0.5f , hitSpawnPoint.transform.position.x - 0.5f), Random.Range(hitSpawnPoint.transform.position.y + 0.5f, hitSpawnPoint.transform.position.y - 0.5f), hitSpawnPoint.transform.position.z), Quaternion.identity);

            CineMachineShake.Instance.ShakeCamera(0.3f, 0.2f);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackTag(collision, "Punch_1", damageManager.punch1, damageManager.punchSound, 1f, true);
        CheckAttackTag(collision, "Punch_2", damageManager.punch2, damageManager.punchSound, 0.8f, true);
        CheckAttackTag(collision, "Kick_1", damageManager.kick1, damageManager.punchSound, 0.6f, true);
        CheckAttackTag(collision, "Kick_2", damageManager.kick2, damageManager.punchSound, 0.5f,true);
        CheckAttackTag(collision, "Final_1", damageManager.final1, damageManager.punchSound, 0.4f, true);
        CheckAttackTag(collision, "PlayersProjectile", damageManager.longDistance, damageManager.longDistanceSfx, 0.3f, false);
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

    #endregion
}

