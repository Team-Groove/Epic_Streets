using UnityEngine;
using UnityEngine.UI;

public class PlayersHealthBar : MonoBehaviour
{
    [SerializeField] public Sprite[] sprites;
    [SerializeField] public Image image;
    private PlayerController controller;
    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        image.sprite = sprites[0];
    }

    private void Update()
    {
        if (controller.currenthealth <= 50)
        {
            image.sprite = sprites[1];
        }
        else if (controller.currenthealth <= 15)
        {
            image.sprite = sprites[2];
        }
        else
        {
            image.sprite = sprites[0];
        }
    }
}
