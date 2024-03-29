using UnityEngine;

public class GoblinProjectile : MonoBehaviour
{
    public float speed;
    public float destroyTime;

    private Rigidbody2D rigidBody;

    private PlayerController target;

    private Vector2 moveDirection;

    public float correctionY;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        target = FindObjectOfType<PlayerController>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
        rigidBody.velocity = new Vector2(moveDirection.x, moveDirection.y + correctionY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerHurtBox")
        {
            Destroy(gameObject);
        }
    }
}
