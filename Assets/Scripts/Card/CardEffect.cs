using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string description;
    public Type type;
    public int applyAmmount;
    public ApplyType applyType;
    public ApplyDelay delay;
    public CardTypeScriptable affectedType;
    public EnemyBlocks block;

    public void DoEffect()
    {
        switch (type)
        {
            case Type.DAMAGE:
                GameManager.Instance.GetEnemy().TakeDamage(applyAmmount, block);
                break;
            case Type.BUFF_CARD:
                GameManager.Instance.SetBuffNextCard(affectedType);
                break;
            case Type.BUFF_COUNTER:
                GameManager.Instance.GetEnemy().BlockNextCommand();
                break; 
            case Type.BUFF_ENERGY:
                switch(delay)
                {
                    case ApplyDelay.IMMEDIATE:
                        Player.Instance.AddEnergy(applyAmmount);
                        break;
                    case ApplyDelay.NEXT_TURN:
                        CardEffect c = new CardEffect();
                        c.applyAmmount = applyAmmount;
                        c.type = Type.BUFF_ENERGY;
                        c.delay = ApplyDelay.IMMEDIATE;
                        c.applyType = ApplyType.FLAT;
                        GameManager.Instance.ApplyEffectNextTurn(c);
                        break;
                }
                break;
        }
    }
}
