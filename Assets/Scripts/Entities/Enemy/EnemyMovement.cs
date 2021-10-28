using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    #region VARIABLES

    private Rigidbody2D rigidBody;
    private EnemyController controller;
    private EnemyIA ia;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        controller = GetComponent<EnemyController>();
        ia = GetComponent<EnemyIA>();
    }

    #endregion



    void Update()
    {
        
    }

    #region FUNCTIONS


    private void FlipSprite()
    {
        if (!controller.isDummy)
        {
            if (!ia.isAttacking)
            {
                if (controller.playersPos.x > transform.position.x)
                {
                    transform.localScale = new Vector3(controller.spriteRendererLocalScaleX, transform.localScale.y, transform.localScale.z);
                }
                else if (controller.playersPos.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-controller.spriteRendererLocalScaleX, transform.localScale.y, transform.localScale.z);
                }
            }
        }
    }

    #endregion
}
