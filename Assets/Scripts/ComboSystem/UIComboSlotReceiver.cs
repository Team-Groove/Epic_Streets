using UnityEngine;
using UnityEngine.EventSystems;


public class UIComboSlotReceiver : MonoBehaviour, IDropHandler
{

    #region VARIABLES

    private RectTransform rectTransform;
    private ComboSystemManager comboManager;
    
    [SerializeField] private AttackClass attack;

    public int slotNumber;
    public string stringName;
    public int attackDamage;
    public bool filled;

    #endregion

    #region UNITY_CALLS
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        comboManager = GetComponentInParent<ComboSystemManager>();
    }
    private void Start()
    {
        stringName = attack.GetStringName();
        attackDamage = attack.GetDamage();
        ComboSlotEvent.eventos.ONSacarAtaqueDeSlot += SacarAtaque;
    }

    #endregion

    #region FUNCTIONS
    private void SacarAtaque(string ataque)
    {
        if (filled)
        {
            if (ataque.Equals(attack.nameOfAttack))
            {
                Debug.Log("SACANDO ATAQUE");
                attack = null;
                stringName = null;
                attackDamage = 0;
                filled = false;
                comboManager.comboSlots[slotNumber] = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        Debug.Log("OnDrop ComboSlot");


        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            attack = eventData.pointerDrag.GetComponent<AttackClass>();
            stringName = attack.GetStringName();
            attackDamage = attack.GetDamage();
            filled = true;


            switch (slotNumber)
            {
                case 0:

                    comboManager.comboSlots[slotNumber] = gameObject.GetComponent<UIComboSlotReceiver>();

                    break;

                case 1:

                    comboManager.comboSlots[slotNumber] = gameObject.GetComponent<UIComboSlotReceiver>();

                    break;

                case 2:

                    comboManager.comboSlots[slotNumber] = gameObject.GetComponent<UIComboSlotReceiver>();

                    break;
            }

        }
    }

    private void OnDestroy()
    {
        ComboSlotEvent.eventos.ONSacarAtaqueDeSlot -= SacarAtaque;
    }

    #endregion
}
