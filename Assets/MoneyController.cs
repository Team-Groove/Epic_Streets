using UnityEngine;
using TMPro;


public class MoneyController : MonoBehaviour
{
    private string moneyPref;
    private PlayerController player;
    [SerializeField]
    public static float currentMoney;
    public static bool canUpgrade;
    
    [SerializeField] private TextMeshProUGUI uiDisplay;


    private void Awake()
    {
        LoadData();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        uiDisplay = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        uiDisplay.text = "Money: " + currentMoney.ToString();

        if (canUpgrade)
        {
            UpgradeMaxLife();
        }
    }

    private void OnDestroy()
    {
        SaveData();
    }

    private void SaveData()
    {
        PlayerPrefs.SetFloat(moneyPref, currentMoney);
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey(moneyPref))
        {
            currentMoney = PlayerPrefs.GetFloat(moneyPref, 0);
        }
        else
        {
            currentMoney = 0;
            PlayerPrefs.SetFloat(moneyPref, 0);
        }
    }
    public void UpgradeMaxLife()
    {
        player.maxhealth += 20;
        currentMoney -= 500;
        player.maxHpUpgraded = true;
        canUpgrade = false;
    }
}
