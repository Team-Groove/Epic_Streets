using UnityEngine;


[ExecuteInEditMode]
public class OrderInLayerEntity : MonoBehaviour
{

    public SpriteRenderer players;

    public bool effect;
  
    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (effect)
        {
            sr.sortingOrder = players.sortingOrder + 1;
        }
        else
        {
            sr.sortingOrder =- (int)(transform.position.y * 100);
        }
       
    }

    


}