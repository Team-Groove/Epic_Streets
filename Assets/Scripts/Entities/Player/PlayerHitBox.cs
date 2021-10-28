using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    #region VARIABLES

    private PlayerAnimation anim;
    private PlayerController player;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        anim = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunch1"))
        {
            anim.SendMessage("DamageRedFeedback");

            player.ReceiveDamage(10);
        }
    }
} 

#endregion




