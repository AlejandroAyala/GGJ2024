using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = System.Random;
using Unity.VisualScripting;
using UnityEditor;
using System;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<CardScriptable> allCards = new List<CardScriptable>();
    [SerializeField]
    private List<CardScriptable> commandCards = new List<CardScriptable>();
    [SerializeField]
    private Enemy currentEnemy;
    private bool isInBattle;
    private CardTypeScriptable buffNextCard;
    private Player player;
    private List<StartingDeck> startingDecks = new List<StartingDeck>();
    private Queue<CardEffect> queuedEffects = new Queue<CardEffect>();

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.F4))
        {
            Battle();
        }
    }

    public void SetBuffNextCard(CardTypeScriptable cardEffect)
    {
        buffNextCard = cardEffect;
    }

    public void ApplyEffectNextTurn(CardEffect cardEffect)
    {
        queuedEffects.Enqueue(cardEffect);
    }

    public new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
        string[] cards = AssetDatabase.FindAssets($"t:{nameof(CardScriptable)}");
        foreach(string card in cards)
        {
            string path = AssetDatabase.GUIDToAssetPath(card);
            CardScriptable c = AssetDatabase.LoadAssetAtPath<CardScriptable>(path);
            bool isCommand = c.cardEffects.Where((effect) => { return effect.type == CardType.COMMAND; }).FirstOrDefault() != null;
            if(isCommand)
            {
                commandCards.Add(c);
            }
            else
            {
                allCards.Add(c);
            }
        }
        string[] decks = AssetDatabase.FindAssets($"t:{nameof(StartingDeck)}");
        foreach (string deck in decks)
        {
            string path = AssetDatabase.GUIDToAssetPath(deck);
            StartingDeck c = AssetDatabase.LoadAssetAtPath<StartingDeck> (path);
            startingDecks.Add(c);
        }
    }



    public CardScriptable GetCommandCardByName(string v)
    {
        return commandCards.Where((x) => x.cardName.Equals(v)).FirstOrDefault();
    }

    public List<CardScriptable> GetAllCards()
    {
        return allCards;
    }


    public void Battle()
    {        
        DeckManager.Instance.SetStartingDeck(startingDecks[UnityEngine.Random.Range(0, startingDecks.Count - 1)]);
        DeckManager.Instance.CreateBattleDeck();
        DeckManager.Instance.ShuffleDeck();
        //TODO: find enemy in scene
        currentEnemy = new GameObject().AddComponent<Enemy>();
        currentEnemy.SetMaxHealth(20);
        currentEnemy.SetHealth(20);
        isInBattle = true;
        PlayPlayerTurn();
    }

    public Enemy GetEnemy()
    {
        if(isInBattle)
        {
            return currentEnemy;
        }
        return null;
    }

    public void EndPlayerTurn()
    {
        PlayKingTurn();
    }

    public void PlayKingTurn()
    {
        currentEnemy.DoActions();
        ApplyDelayedEffects();
        PlayPlayerTurn();
    }

    private void PlayPlayerTurn()
    {
        DeckManager.Instance.DrawCards(3);
    }

    private void ApplyDelayedEffects()
    {
        foreach(CardEffect c in queuedEffects)
        {
            c.DoEffect();
        }
    }
}
