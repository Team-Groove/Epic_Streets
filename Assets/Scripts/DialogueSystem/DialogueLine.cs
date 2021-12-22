using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;

public class DialogueLine : DialogueBaseClass
{
    private TextMeshProUGUI textHolder;
    [SerializeField] private string input;

    [SerializeField] private float delay;

    [SerializeField] private Sprite characterSprite;
    [SerializeField] private Image imageHolder;

    private bool canTalkTo = true;

    public bool canUpgrade;

    private IEnumerator lineAppear;

    private void Awake()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
        textHolder.text = "";

        imageHolder.sprite = characterSprite;
        imageHolder.preserveAspect = true;
    }

    private void OnEnable()
    {
        ResetLine();
        lineAppear = (WriteText(input, textHolder, delay));
        StartCoroutine(lineAppear);
    }

    private void OnDisable()
    {
        if (canUpgrade && MoneyController.currentMoney >= 500)
        {
            MoneyController.canUpgrade = true;
        }
    }

    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (textHolder.text != input)
            {
                StopCoroutine(lineAppear);
                textHolder.text = input;
                lineAppear = null;
            }
            else finished = true;
        }
    }
    
    private void ResetLine()
    {
        textHolder = GetComponent<TextMeshProUGUI>();
        textHolder.text = "";
        finished = false;
    }

}
