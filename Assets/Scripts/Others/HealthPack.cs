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
        if (player.currenthealth == player.maxhealth)
        {
            return;
        }
        else 
        {

            player.currenthealth += healthToPlayer;

            if (player.currenthealth > player.maxhealth)
            {
                player.currenthealth = player.maxhealth;
            }
                
            PlayerPrefs.SetFloat(playerHpPrefName, player.currenthealth);
        }
     
    }

}
