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
    private EnemyBlocks nextBlock = EnemyBlocks.BLOCK_GREEN;
    private EnemyCommands nextCommand = EnemyCommands.NONE;

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
        if (blockType == nextBlock)
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

    void ChooseActions()
    {
        Random r = new Random();
        var v = Enum.GetValues(typeof(EnemyCommands));
        nextCommand = (EnemyCommands)v.GetValue(r.Next(v.Length-1));
        v = Enum.GetValues(typeof(EnemyBlocks));
        nextBlock = (EnemyBlocks)v.GetValue(r.Next(v.Length));
    }

    public void DoActions()
    {
        switch(nextCommand)
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
                CardScriptable c = GameManager.Instance.GetCommandCardByName("Guards!");
                DeckManager.Instance.AddBattleCardToDeck(c);
                break;
            case EnemyCommands.FOOD:
                break;
            case EnemyCommands.HERECY:
                break;
            case EnemyCommands.ORDER:
                break;
            case EnemyCommands.SILENCE:
                break;
        }
        
        ChooseActions();
    }
}
