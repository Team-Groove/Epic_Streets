using UnityEngine;
using UnityEngine.EventSystems;

public class UIBasicSlot : MonoBehaviour, IDropHandler
{

    #region VARIABLES
    
    protected RectTransform rectTransform;

    #endregion

    #region UNITY_CALLS

    protected void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    #endregion

    #region FUNCTIONS
    
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        }
    }

    #endregion
}
