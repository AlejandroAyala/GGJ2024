using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Linq;

public class DeckManager : Singleton<DeckManager>
{

    [SerializeField]
    private List<CardScriptable> currentBattleDeck = new List<CardScriptable>();
    [SerializeField]
    private List<CardScriptable> playerDeck = new List<CardScriptable>();
    [SerializeField]
    private List<Card> activeHand = new List<Card>();
    [SerializeField]
    private List<CardScriptable> discardPile = new List<CardScriptable>();
  

    public void GenerateRandomDeck(int cardQuantity)
    {
        playerDeck.Clear();
        for (int i = 0; i < cardQuantity; i++)
        {
            playerDeck.Add(GameManager.Instance.GetAllCards()[UnityEngine.Random.Range(0, GameManager.Instance.GetAllCards().Count - 1)]);
        }
    }

    public void DrawCards(int ammount)
    {
        ammount = Mathf.Min(ammount, currentBattleDeck.Count);
        for (int i = 0; i < ammount; i++)
        {
            Card c = UIManager.Instance.InstantiateCard(currentBattleDeck[0]);
            activeHand.Add(c);
            currentBattleDeck.RemoveAt(0);
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

    internal void CreateBattleDeck()
    {
        currentBattleDeck.Clear();
        currentBattleDeck.AddRange(playerDeck);
    }

    public void ShuffleDeck()
    {
        Random random = new();
        currentBattleDeck = currentBattleDeck.OrderBy(x => random.Next()).ToList();
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

    public void AddBattleCardToDeck(CardScriptable card)
    {
        currentBattleDeck.Add(card);
    }


}
