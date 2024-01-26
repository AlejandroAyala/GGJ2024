using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string description;
    public Type type;
    public float applyAmmount;
    public ApplyType applyType;

    public void DoEffect()
    {
        switch (type)
        {
            case Type.DAMAGE:
                break;
            case Type.BUFF_BLUE:
                break;
            case Type.BUFF_RED:
                break;
            case Type.BUFF_GREEN:
                break;
            case Type.BUFF_COUNTER:
                break;
            case Type.BUFF_ENERGY:
                break;
        }
    }
}
