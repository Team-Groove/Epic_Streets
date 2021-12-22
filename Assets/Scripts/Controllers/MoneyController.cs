using UnityEngine;
using TMPro;


public class MoneyController : MonoBehaviour
{
    private string moneyPref;
    private PlayerController player;
    [SerializeField]
    public static float currentMoney;
    public static bool canUpgrade;

    private SceneController sceneController;

    [SerializeField] private TextMeshProUGUI uiDisplay;
    [SerializeField] private TextMeshProUGUI newgameplus;

    private void Awake()
    {

        sceneController = FindObjectOfType<SceneController>();

        if (sceneController.endGameNum > 0)
        {
            newgameplus.gameObject.SetActive(true);
            newgameplus.text = "New Game+ " + sceneController.endGameNum;
        }
        else
        {
            newgameplus.text = null;
            newgameplus.gameObject.SetActive(false);
        }

        LoadData();

        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        uiDisplay = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        uiDisplay.text = "Points: " + currentMoney.ToString();

        if (canUpgrade)
        {
            UpgradeMaxLife();
        }

        if (player.IsDead)
        {
            currentMoney = 0;
            PlayerPrefs.SetFloat(moneyPref, 0);
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
