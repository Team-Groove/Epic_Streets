using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    [SerializeField]
    private PlayerAnimation anim;
    private PlayerController player;
    private void Awake()
    {
        anim = GetComponentInParent<PlayerAnimation>();
        player = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyPunch1"))
        {
            anim.SendMessage("RedFeedback");

            player.ReceiveDamage(10);

        }
    }
    
}

