using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private EnemyController controller;
    private EnemyIA ia;

    private float spriteRendererLocalScaleX;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        controller = GetComponent<EnemyController>();
        ia = GetComponent<EnemyIA>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        spriteRendererLocalScaleX = spriteRenderer.transform.localScale.x;
    }
    private void Update()
    {
        FlipSprite();
        InCaseOfDead();
    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }

    #endregion

    #region FUNCTIONS

  
    private void FlipSprite()
    {
        if (!controller.IsDead)
        {
            if (!controller.isDummy)
            {
                if (!ia.isAttacking)
                {
                    if (controller.playersPos.x > transform.position.x)
                    {
                        spriteRenderer.transform.localScale = new Vector3(spriteRendererLocalScaleX, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                    }
                    else if (controller.playersPos.x < transform.position.x)
                    {
                        spriteRenderer.transform.localScale = new Vector3(-spriteRendererLocalScaleX, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
                    }
                }
            }
        }
    }
    private void MoveCharacter()
    {
        if (ia.chasingPlayer && !controller.isStunned && !controller.IsDead)
        {
            rigidBody.velocity = new Vector2(ia.GetPlayerPos().x * controller.horizontal_speed, ia.GetPlayerPos().y * controller.vertical_speed);
        }
    }
    private void InCaseOfDead()
    {
        if (controller.IsDead)
        {
            StopMoving();
        }
    }
    public void StopMoving()
    {
        rigidBody.velocity = Vector2.zero;
    }

    public IEnumerator EnemyDamageRedFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }
    

    #endregion
}
