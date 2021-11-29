using UnityEngine;

public class HitEffect : MonoBehaviour
{
    #region VARIABLES

    public float delayBeforeDestroy;

    #endregion

    #region UNITY_CALLS


    private void Start()
    {
        Destroy(gameObject, delayBeforeDestroy);
    }

    #endregion
}
