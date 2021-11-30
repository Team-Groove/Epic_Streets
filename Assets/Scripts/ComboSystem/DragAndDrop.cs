using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class DragAndDrop : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IDropHandler
{

    #region VARIABLES

    private RectTransform rectTrasform;
    private CanvasGroup canvasGroup;
    private AttackClass ataque;

    public Vector3 defaultPos;
    public Vector3 tempPos;

    [SerializeField] Canvas canvas;

    public bool inDropZone;
    public bool droppedOnSlot;
    public bool droppedOnAnotherDragAndDrop;

    #endregion

    #region UNITY_CALLS

    private void Start()
    {
        defaultPos = GetComponent<RectTransform>().localPosition;
    }

    private void Awake()
    {
        rectTrasform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        ataque = GetComponent<AttackClass>();
    }

    #endregion

    #region EVENTS

    public void OnEndDrag(PointerEventData eventData)
    {
        
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(CheckIfDroppedOnSlot(eventData));
    }


    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTrasform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
   
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<DragAndDrop>().droppedOnAnotherDragAndDrop = true;

            tempPos = defaultPos;
            defaultPos = eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPos;
            eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPos = tempPos;

            transform.localPosition = defaultPos;
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = eventData.pointerDrag.GetComponent<DragAndDrop>().defaultPos;
        
            
        }
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        droppedOnSlot = false;
        droppedOnAnotherDragAndDrop = false;
        
        ComboSlotEvent.eventos.SacarAtaqueDeSlot(ataque.nameOfAttack);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public IEnumerator CheckIfDroppedOnSlot(PointerEventData data)
    {
        yield return new WaitForEndOfFrame();
        if (!droppedOnSlot && !droppedOnAnotherDragAndDrop)
        {
            transform.localPosition = defaultPos;
        }
    }

    #endregion

}
