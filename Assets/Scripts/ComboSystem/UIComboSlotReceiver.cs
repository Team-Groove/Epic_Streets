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
    
    public DragAndDrop[] attacks;

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

    private void Update()
    {
        for (int i = 0; i < attacks.Length; i++)
        {
            if (attacks[i].transform.localPosition == transform.localPosition)
            {
                stringName = attacks[i].GetComponent<AttackClass>().nameOfAttack;
            }
        }
    }

    #endregion

    #region FUNCTIONS
    private void SacarAtaque(string ataque)
    {
        if (filled)
        {
            if (ataque.Equals(attack.nameOfAttack))
            {
             
                attack = null;
                stringName = null;
                attackDamage = 0;
                filled = false;
               // comboManager.comboSlots[slotNumber] = null;
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {

        eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnSlot = true;
        eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPos = transform.localPosition;

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
