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
    private CardTypeScriptable buffNextCard;
    private Player player;
    private List<StartingDeck> startingDecks = new List<StartingDeck>();

    private Queue<CardEffect> queuedEffects = new Queue<CardEffect>();


    internal void SetBuffNextCard(CardTypeScriptable cardEffect)
    {
        buffNextCard = cardEffect;
    }

    internal void ApplyEffectNextTurn(CardEffect cardEffect)
    {
        queuedEffects.Enqueue(cardEffect);
    }

    public void Awake()
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
        string[] decks = AssetDatabase.FindAssets($"t:{nameof(StartingDeck)}");
        foreach (string deck in decks)
        {
            string path = AssetDatabase.GUIDToAssetPath(deck);
            StartingDeck c = AssetDatabase.LoadAssetAtPath<StartingDeck> (path);
            startingDecks.Add(c);
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
