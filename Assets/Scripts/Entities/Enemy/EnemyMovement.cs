using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES

    private GameObject spriteRenderer;
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
        spriteRenderer = GameObject.Find("SpriteRenderer");
    }
    private void Start()
    {
        spriteRendererLocalScaleX = spriteRenderer.transform.localScale.x;
    }
    private void Update()
    {
        FlipSprite();       
    }
    private void FixedUpdate()
    {
        MoveCharacter();
    }

    #endregion

    #region FUNCTIONS

  
    private void FlipSprite()
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
    private void MoveCharacter()
    {
        if (ia.chasingPlayer)
        {
            rigidBody.velocity = new Vector2(ia.GetPlayerPos().x * controller.horizontal_speed, ia.GetPlayerPos().y * controller.vertical_speed);
        }
        
    }
    public void StopMoving()
    {
        rigidBody.velocity = Vector2.zero;
    }

    #endregion
}
