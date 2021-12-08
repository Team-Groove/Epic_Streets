using UnityEngine;


[ExecuteInEditMode]
public class OrderInLayerEntity : MonoBehaviour
{

    public SpriteRenderer players = null;
    public TrailRenderer trail = null;

    public bool effectOnTop;
    public bool effectOnBot;

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (effectOnTop)
        {
            sr.sortingOrder = players.sortingOrder + 1;
        }
        else if (effectOnBot)
        {
            trail.sortingOrder = players.sortingOrder - 1;
        }
        else
        {
            sr.sortingOrder =- (int)(transform.position.y * 100);
        }
       
    }

    


}