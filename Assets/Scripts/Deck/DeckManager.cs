using System.Collections.Generic;
using UnityEngine;
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
    public int maxHandCards = 7;

    public void Update()
    {
        UIManager.Instance.SetDeckAmmount(currentBattleDeck.Count);
        UIManager.Instance.SetDiscardAmmount(discardPile.Count);
    }

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
        int allowedCards = maxHandCards - activeHand.Count;
        ammount = Mathf.Min(allowedCards, currentBattleDeck.Count, ammount);
        for (int i = 0; i < ammount; i++)
        {
            Card c = UIManager.Instance.InstantiateCard(currentBattleDeck[0]);
            CardEffect blockEffect = c.cardInfo.cardEffects.Where((x) => { return x.type == CardType.BLOCK_CARD; }).FirstOrDefault();
            if (blockEffect != null)
            {
                GameManager.Instance.AddLock(blockEffect.affectedType);
            }
            activeHand.Add(c);
            currentBattleDeck.RemoveAt(0);
        }
        if(currentBattleDeck.Count == 0)
        {
            currentBattleDeck.AddRange(discardPile);
            discardPile.Clear();
            ShuffleDeck();
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
        currentBattleDeck = currentBattleDeck.OrderBy(x => Random.Range(0,10000)).ToList();
    }

    public void OrderDeck()
    {
        playerDeck = playerDeck.OrderBy(x => x.name).ToList();
    }

    public void AddCardToDeck(CardScriptable card)
    {
        playerDeck.Add(card);
    }

    public void AddCardsToDeck(CardScriptable card, int ammount)
    {
        for(int i = 0; i < ammount; i++)
        {
            playerDeck.Add(card);
        }        
    }

    public void AddBattleCardToDeck(CardScriptable card)
    {
        currentBattleDeck.Add(card);
    }

    public void DiscardRandomCards(int x)
    {
        List<Card> ableToDiscard = activeHand.Where((x) => { return !x.cardInfo.cardType.Equals(CardType.COMMAND); }).ToList();
        int discard = Mathf.Min(ableToDiscard.Count, x);
        for (int i = 0; i < discard; i++)
        { 
            int randIdx = Random.Range(0, ableToDiscard.Count-1);
            Card card = ableToDiscard[randIdx];
            RemoveFromHand(card);            
            AddToDiscardPile(card.cardInfo);
            UIManager.Instance.ReturnCardToQueue(card);
        }
    }

    internal void SetStartingDeck(StartingDeck startingDeck)
    {
        playerDeck.Clear();
        if(startingDeck.startingCards.Count != startingDeck.cardQuantity.Count)
        {
            Debug.LogError("Starting deck missconfigured");
        }

        for(int i = 0; i < startingDeck.startingCards.Count; i++)
        {
            AddCardsToDeck(startingDeck.startingCards[i], startingDeck.cardQuantity[i]);
        }
    }

    public void AddBattleCardToHand(CardScriptable card)
    {
        if(activeHand.Count >= maxHandCards)
        {
            int discardAmmount = (activeHand.Count - maxHandCards) + 1;
            DiscardRandomCards(discardAmmount);
        }
        Card c = UIManager.Instance.InstantiateCard(card);
        CardEffect blockEffect = card.cardEffects.Where((x) => { return x.type == CardType.BLOCK_CARD; }).FirstOrDefault();
        if (blockEffect != null)
        {
            GameManager.Instance.AddLock(blockEffect.affectedType);
        }
        activeHand.Add(c);
    }
}
