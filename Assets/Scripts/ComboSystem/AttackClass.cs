using UnityEngine;

public class AttackClass : MonoBehaviour
{
    #region VARIABLES
    public int damage;
    public string nameOfAttack;
    #endregion

    #region FUNCTIONS

    public void SetAttack(int damage, AnimationClip animation, string name)
    {
        this.damage = damage;
        this.nameOfAttack = name;
    }
 
    public int GetDamage()
    {
        return damage;
    }
    public string GetStringName()
    {
        return nameOfAttack;
    }

    #endregion

}
