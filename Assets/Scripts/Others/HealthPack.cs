using UnityEngine;

public class HealthPack : MonoBehaviour
{
    private string playerHpPrefName = "PlayerHp";

    private PlayerController player;

    public float healthToPlayer;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.Play("PickUp");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        player.currenthealth += healthToPlayer;
        PlayerPrefs.SetFloat(playerHpPrefName, player.currenthealth);
    }

}
