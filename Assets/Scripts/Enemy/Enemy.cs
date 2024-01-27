using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHp;

    public void SetMaxHealth(int hp)
    {
        this.maxHp = hp;
    }

    public void SetHealth(int hp)
    {
        this.hp = hp;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        hp = Mathf.Max(0, hp);
    }

    public void Heal(int ammount)
    {
        hp += ammount;
        hp = Mathf.Min(hp, maxHp);
    }
}
