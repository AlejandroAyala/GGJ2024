using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string description;
    public CardType type;
    public int applyAmmount;
    public ApplyType applyType;
    public ApplyDelay delay;
    public CardTypeScriptable affectedType;
    public EnemyBlocks block;

    public void DoEffect()
    {
        CardEffect buff = GameManager.Instance.GetCurrentBuff();
        switch (type)
        {
            case CardType.DAMAGE:
                int newApply = applyAmmount;
                if(buff != null && buff.affectedType == affectedType)
                {
                    switch(buff.applyType)
                    {
                        case ApplyType.FLAT:
                            newApply = newApply + buff.applyAmmount;
                            break;
                        case ApplyType.PERCENTAGE:
                            newApply = newApply * (buff.applyAmmount / 100);
                            break;
                    }
                }
                GameManager.Instance.GetEnemy().TakeDamage(newApply, block);
                break;
            case CardType.BUFF_CARD:
                GameManager.Instance.SetBuffNextCard(this);
                break;
            case CardType.BUFF_COUNTER:
                GameManager.Instance.GetEnemy().BlockNextCommand();
                break; 
            case CardType.BUFF_ENERGY:
                switch(delay)
                {
                    case ApplyDelay.IMMEDIATE:
                        Player.Instance.AddEnergy(applyAmmount);
                        break;
                    case ApplyDelay.NEXT_TURN:
                        CardEffect c = new CardEffect();
                        c.applyAmmount = applyAmmount;
                        c.type = CardType.BUFF_ENERGY;
                        c.delay = ApplyDelay.IMMEDIATE;
                        c.applyType = ApplyType.FLAT;
                        GameManager.Instance.ApplyEffectNextTurn(c);
                        break;
                }
                break;
            case CardType.BLOCK_CARD:
                GameManager.Instance.RemoveLock(affectedType);
                break;
        }
    }
}
