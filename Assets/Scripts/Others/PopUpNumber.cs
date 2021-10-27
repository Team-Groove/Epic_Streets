using TMPro;
using UnityEngine;

public class PopUpNumber : MonoBehaviour
{
    #region VARIABLES

    [SerializeField] private TextMeshPro text;

    #endregion

    #region UNITY_CALLS

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    private void Start()
    {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
    }

    #endregion
}
