using UnityEngine;

public class ComboSystemManager : MonoBehaviour
{

    #region VARIABLES

    public UIComboSlotReceiver[] comboSlots;
    public string[] comboStrings;
    public int[] attackDamage;

    #endregion

    #region UNITY_CALLS

    private void Update()
    {
        //SITUACIONES HARDCODEADAS PORQUE NO SE COMO SE USA BIEN EL FOR (ESTARIA BUENO ARREGLARLO)
        
        SituationsOfOne();
        SituationOfTwo();
        SituationOfThree();
    }

    #endregion

    #region FUNCTIONS
    private void SituationOfThree()
    {
        if (comboSlots[0] != null && comboSlots[1] != null && comboSlots[2] != null)
        {

            comboStrings[0] = comboSlots[0].stringName;
            attackDamage[0] = comboSlots[0].attackDamage;

            comboStrings[1] = comboSlots[1].stringName;
            attackDamage[1] = comboSlots[1].attackDamage;

            comboStrings[2] = comboSlots[2].stringName;
            attackDamage[2] = comboSlots[2].attackDamage;
        }
    }
    private void SituationsOfOne()
    {
        if (comboSlots[0] != null && comboSlots[1] == null && comboSlots[2] == null)
        {
            for (int i = 0; i < comboSlots.Length; i++)
            {
                comboStrings[i] = comboSlots[0].stringName;
                attackDamage[i] = comboSlots[0].attackDamage;
            }
        }
        else if (comboSlots[0] == null && comboSlots[1] != null && comboSlots[2] == null)
        {
            for (int i = 0; i < comboSlots.Length; i++)
            {
                comboStrings[i] = comboSlots[1].stringName;
                attackDamage[i] = comboSlots[1].attackDamage;
            }
        }
        else if (comboSlots[0] == null && comboSlots[1] == null && comboSlots[2] != null)
        {
            for (int i = 0; i < comboSlots.Length; i++)
            {
                comboStrings[i] = comboSlots[2].stringName;
                attackDamage[i] = comboSlots[2].attackDamage;
            }
        }
    }
    private void SituationOfTwo()
    {
        if (comboSlots[0] != null && comboSlots[1] != null && comboSlots[2] == null)
        {

            comboStrings[0] = comboSlots[0].stringName;
            attackDamage[0] = comboSlots[0].attackDamage;

            comboStrings[1] = comboSlots[1].stringName;
            attackDamage[1] = comboSlots[1].attackDamage;

            comboStrings[2] = comboSlots[1].stringName;
            attackDamage[2] = comboSlots[1].attackDamage;
        }
        else if (comboSlots[0] == null && comboSlots[1] != null && comboSlots[2] != null)
        {
            comboStrings[0] = comboSlots[2].stringName;
            attackDamage[0] = comboSlots[2].attackDamage;

            comboStrings[1] = comboSlots[1].stringName;
            attackDamage[1] = comboSlots[1].attackDamage;

            comboStrings[2] = comboSlots[2].stringName;
            attackDamage[2] = comboSlots[2].attackDamage;
        }
        else if (comboSlots[0] != null && comboSlots[1] == null && comboSlots[2] != null)
        {
            comboStrings[0] = comboSlots[0].stringName;
            attackDamage[0] = comboSlots[0].attackDamage;

            comboStrings[1] = comboSlots[0].stringName;
            attackDamage[1] = comboSlots[0].attackDamage;

            comboStrings[2] = comboSlots[2].stringName;
            attackDamage[2] = comboSlots[2].attackDamage;
        }
    }
    #endregion
}
