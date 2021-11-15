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

    [SerializeField] private AudioClip sfxSounds;

    public bool test;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
        sprite = GetComponentInParent<EnemyMovement>();
    }

    private void Update()
    {
        if (test)
        {
            enemy.StartCoroutine("EnemyDamageRedFeedback");
        }
    }

    #endregion

    #region FUNCTIONS

    private void CheckAttackTag(Collider2D collision, string nameOfAttack, int dmgReceived)
    {
        if (collision.gameObject.CompareTag(nameOfAttack))
        {
            sprite.StartCoroutine("EnemyDamageRedFeedback");
            enemy.StartCoroutine("GetStunned");
            enemy.ReceiveDamage(dmgReceived);
            SFXController.instance.PlaySound(sfxSounds, 0.3f, Random.Range(0.6f, 0.8f));
            popUpNumber.GetComponentInChildren<TextMeshPro>().SetText("-" + dmgReceived.ToString());
            Instantiate(popUpNumber, popUpNumberSpawnPoint.transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAttackTag(collision, "Punch_1", damageManager.punch1);
        CheckAttackTag(collision, "Punch_2", damageManager.punch2);
        CheckAttackTag(collision, "Kick_1", damageManager.kick1);
        CheckAttackTag(collision, "Kick_2", damageManager.kick2);
        CheckAttackTag(collision, "Final_1", damageManager.final1);
        CheckAttackTag(collision, "PlayersProjectile", damageManager.longDistance);
    }
 
    #endregion
}

