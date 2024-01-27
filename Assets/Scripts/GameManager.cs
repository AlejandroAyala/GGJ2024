using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;
using Unity.VisualScripting;
using UnityEditor;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<CardScriptable> allCards = new List<CardScriptable>();
    [SerializeField]
    private List<CardScriptable> commandCards = new List<CardScriptable>();
    [SerializeField]
    private Enemy currentEnemy;
    private bool isInBattle;

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            DeckManager.Instance.ShuffleDeck();
        }
        if(Input.GetKeyUp(KeyCode.F4))
        {
            DeckManager.Instance.GenerateRandomDeck(15);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            DeckManager.Instance.DrawCards(1);
        }
        if(Input.GetKeyUp(KeyCode.F12))
        {
            Battle();
        }
    }

    public void OnEnable()
    {
        string[] cards = AssetDatabase.FindAssets($"t:{nameof(CardScriptable)}");
        foreach(string card in cards)
        {
            string path = AssetDatabase.GUIDToAssetPath(card);
            CardScriptable c = AssetDatabase.LoadAssetAtPath<CardScriptable>(path);
            bool isCommand = c.cardEffects.Where((effect) => { return effect.type == Type.COMMAND; }).FirstOrDefault() != null;
            if(isCommand)
            {
                commandCards.Add(c);
            }
            else
            {
                allCards.Add(c);
            }
        }
    }

    public List<CardScriptable> GetAllCards()
    {
        return allCards;
    }


    public void Battle()
    {
        DeckManager.Instance.CreateBattleDeck();
        currentEnemy = new GameObject().AddComponent<Enemy>();
        currentEnemy.SetMaxHealth(20);
        currentEnemy.SetHealth(20);
        isInBattle = true;
    }

    public Enemy GetEnemy()
    {
        if(isInBattle)
        {
            return currentEnemy;
        }
        return null;
    }
}
