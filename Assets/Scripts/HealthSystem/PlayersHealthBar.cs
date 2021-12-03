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
        image = GameObject.Find("UIController").gameObject.transform.Find("HealthBar").gameObject.transform.Find("Image").GetComponent<Image>();
        image.sprite = sprites[0];
    }

    private void Update()
    {

        if (controller.currenthealth > controller.maxhealth / 2)
        {
            image.sprite = sprites[0];
        }
        else if (controller.currenthealth > controller.maxhealth / 3)
        {
            image.sprite = sprites[1];
        }
        else if (controller.currenthealth > controller.maxhealth / 10)
        {
            image.sprite = sprites[2];
        }
     
    }

    public void FindHealthBarSprite()
    {
        if (image == null && GameObject.Find("UIController") != null)
        {
            image = GameObject.Find("UIController").gameObject.transform.Find("HealthBar").gameObject.transform.Find("Image").GetComponent<Image>();
        }
        else
        {
            return;
        }
    }
}
