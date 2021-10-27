using System;
using UnityEngine;

public class ComboSlotEvent : MonoBehaviour
{
    public static ComboSlotEvent eventos;
    
    private void Awake()
    {
        eventos = this;
    }

    public event Action<string> ONSacarAtaqueDeSlot;

    public void SacarAtaqueDeSlot(string nombreAtaque)
    {
        if (ONSacarAtaqueDeSlot != null)
        {
            ONSacarAtaqueDeSlot(nombreAtaque);
        }
    }
}
