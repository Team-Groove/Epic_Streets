using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI hp;

    private RectTransform rect;

    #endregion
    
    #region UNITY_CALLS
    private void Start()
    {
        slider = GetComponentInChildren<Slider>();
        rect = GetComponent<RectTransform>();
    }

    #endregion

    #region PUBLIC_FUNCTIONS

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void UpdateHealth(int health)
    {
        slider.value = health;
        hp.SetText(health.ToString());
    }

    #endregion

    
}