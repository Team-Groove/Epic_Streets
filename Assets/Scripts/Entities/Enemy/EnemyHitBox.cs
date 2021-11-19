using UnityEngine;
using TMPro;
using System.Collections;

public class EnemyHitBox : MonoBehaviour
{
    #region VARIABLES
    
    private EnemyController enemy;
    private AttackDamageManager damageManager;
    private EnemyMovement sprite;

    [SerializeField] private GameObject popUpNumber;
    [SerializeField] private GameObject popUpNumberSpawnPoint;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
        sprite = GetComponentInParent<EnemyMovement>();
    }

    #endregion

    #region FUNCTIONS

    private void CheckAttackTag(Collider2D collision, string nameOfAttack, int dmgReceived, AudioClip audio, float pitch,bool canStun)
    {
        if (collision.gameObject.CompareTag(nameOfAttack))
        {
            
            if (canStun && !enemy.isDummy)
            {
                sprite.StartCoroutine("EnemyDamageRedFeedback");
                enemy.StartCoroutine("GetStunned");
            }
            else
            {
                enemy.StartCoroutine("EnemyDamageRedFeedback");
            }
            enemy.ReceiveDamage(dmgReceived);
            SFXController.instance.PlaySound(audio, 0.3f, pitch);
            popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + dmgReceived.ToString());
            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);
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
 
    #endregion
}

