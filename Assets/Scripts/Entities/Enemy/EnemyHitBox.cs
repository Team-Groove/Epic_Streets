using UnityEngine;
using TMPro;

public class EnemyHitBox : MonoBehaviour
{
    #region VARIABLES
    
    private BoxCollider2D boxCollider2D;
    private EnemyController enemy;
    private AttackDamageManager damageManager;

    [SerializeField] private GameObject popUpNumber;
    [SerializeField] private GameObject popUpNumberSpawnPoint;
    
    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemy = GetComponentInParent<EnemyController>();
        damageManager = FindObjectOfType<AttackDamageManager>();
    }

    #endregion

    #region FUNCTIONS

    private void CheckAttackTag(Collider2D collision, string nameOfAttack, int dmgReceived)
    {
        if (collision.gameObject.CompareTag(nameOfAttack))
        {
            enemy.SendMessage("RedFeedback");
            enemy.ReceiveDamage(dmgReceived);
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

