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
    private List<CardScriptable> currentDeck = new List<CardScriptable>();

    private List<CardScriptable> playerDeck = new List<CardScriptable>();
    private List<CardScriptable> allCards = new List<CardScriptable>();
    private List<Card> activeHand = new List<Card>();
    private List<CardScriptable> discardPile = new List<CardScriptable>();

    public void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            ShuffleDeck();
        }
        if(Input.GetKeyUp(KeyCode.F4))
        {
            GenerateRandomDeck(5);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            //get first card
            if(playerDeck.Count > 0)
            {
                DrawCards(1);
            }
        }
    }

    public void OnEnable()
    {
        string[] cards = AssetDatabase.FindAssets($"t:{nameof(CardScriptable)}");
        foreach(string card in cards)
        {
            string path = AssetDatabase.GUIDToAssetPath(card);
            CardScriptable c = AssetDatabase.LoadAssetAtPath<CardScriptable>(path);
            allCards.Add(c);
        }
        GenerateRandomDeck(5);
    }

    public void ShuffleDeck()
    {
        Random random = new();
        playerDeck = playerDeck.OrderBy(x => random.Next()).ToList();
        random = null;
    }

    public void OrderDeck()
    {
        playerDeck = playerDeck.OrderBy(x => x.name).ToList();
    }

    public void AddCardToDeck(CardScriptable card)
    {
        playerDeck.Add(card);
    }

    public void GenerateRandomDeck(int cardQuantity)
    {
        playerDeck.Clear();
        for(int i = 0; i<cardQuantity;i++)
        {
            playerDeck.Add(allCards[UnityEngine.Random.Range(0,allCards.Count-1)]);
        }
    }

    public void DrawCards(int ammount)
    {
        ammount = Mathf.Min(ammount, playerDeck.Count);
        for (int i = 0; i < ammount; i++)
        {
            Card c = UIManager.Instance.InstantiateCard(playerDeck[0]);
            activeHand.Add(c);
            playerDeck.RemoveAt(0);
        }
    }

    public void AddToDiscardPile(CardScriptable card)
    {
        discardPile.Add(card);
    }

    public void RemoveFromHand(Card c)
    {
        activeHand.Remove(c);
    }
}
