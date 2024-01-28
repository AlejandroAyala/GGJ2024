using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private EnemyBlocks nextBlock = EnemyBlocks.NONE;
    [SerializeField]
    private EnemyCommands nextCommand = EnemyCommands.NONE;
    [SerializeField]
    private EnemyBlocks currentBlock = EnemyBlocks.NONE;

    public void SetMaxHealth(int hp)
    {
        this.maxHp = hp;
    }

    public void SetHealth(int hp)
    {
        this.hp = hp;
    }

    public void TakeDamage(int damage, EnemyBlocks blockType)
    {
        if (blockType == currentBlock)
        {
            return;
        }
        hp -= damage;
        hp = Mathf.Max(0, hp);
    }

    public void Heal(int ammount)
    {
        hp += ammount;
        hp = Mathf.Min(hp, maxHp);
    }

    internal void BlockNextCommand()
    {
        nextCommand = EnemyCommands.NONE;
    }

    public void ChooseActions()
    {
        Random r = new Random();
        var v = Enum.GetValues(typeof(EnemyCommands));
        nextCommand = (EnemyCommands)v.GetValue(r.Next(v.Length-1));
        v = Enum.GetValues(typeof(EnemyBlocks));
        nextBlock = (EnemyBlocks)v.GetValue(r.Next(v.Length)-1);
    }

    public void DoActions()
    {
        CardScriptable c;
        switch (nextCommand)
        {
            case EnemyCommands.NONE:
                break;
            case EnemyCommands.LIGHT_HEAL:
                Heal(5);
                break;
            case EnemyCommands.MEDIUM_HEAL:
                Heal(10);
                break;
            case EnemyCommands.HARD_HEAL:
                Heal(15);
                break;
            case EnemyCommands.GUARDS:
                c = GameManager.Instance.GetCommandCardByName("Guards!");
                DeckManager.Instance.AddBattleCardToHand(c);
                break;
            case EnemyCommands.FOOD:
                c = GameManager.Instance.GetCommandCardByName("Food!");
                DeckManager.Instance.AddBattleCardToHand(c);
                break;
            case EnemyCommands.HERECY:
                c = GameManager.Instance.GetCommandCardByName("Heresy!");
                DeckManager.Instance.AddBattleCardToDeck(c);
                break;
            case EnemyCommands.ORDER:
                DeckManager.Instance.DiscardRandomCards(2);
                break;
            case EnemyCommands.SILENCE:
                c = GameManager.Instance.GetCommandCardByName("Silence!");
                DeckManager.Instance.AddBattleCardToHand(c);
                break;
        }
        currentBlock = nextBlock;
        ChooseActions();
    }
}
