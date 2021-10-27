using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IEndDragHandler, IDragHandler, IBeginDragHandler
{

    #region VARIABLES

    private RectTransform rectTrasform;
    private CanvasGroup canvasGroup;
    private AttackClass ataque;

    [SerializeField] Canvas canvas;

    public bool inDropZone;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        rectTrasform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        ataque = GetComponent<AttackClass>();
    }

    #endregion

    #region EVENTS

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("OnPointerDown");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTrasform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        ComboSlotEvent.eventos.SacarAtaqueDeSlot(ataque.nameOfAttack);
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    #endregion

}
