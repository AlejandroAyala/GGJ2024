using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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
    private CardEffect buffNextCard;
    private Player player;
    private List<StartingDeck> startingDecks = new List<StartingDeck>();
    private Queue<CardEffect> queuedEffects = new Queue<CardEffect>();
    [SerializeField]
    public List<CardTypeScriptable> locks = new List<CardTypeScriptable>();

    public void SetBuffNextCard(CardEffect cardEffect)
    {
        buffNextCard = cardEffect;
    }

    public CardEffect GetCurrentBuff()
    {
        CardEffect retVal = buffNextCard;
        buffNextCard = null;
        return retVal;
    }

    public void ApplyEffectNextTurn(CardEffect cardEffect)
    {
        queuedEffects.Enqueue(cardEffect);
    }

    public new void Awake()
    {
        base.Awake();
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

    internal void RemoveLock(CardTypeScriptable affectedType)
    {
        for(int i = 0; i<locks.Count;i++)
        {
            if (locks[i]== affectedType)
            {
                locks.RemoveAt(i);
                break;
            }
        }
    }

    internal bool GetLock(CardTypeScriptable cardType)
    {
        return locks.Where((x) => { return x == cardType; }).FirstOrDefault() != null;
    }

    public void AddLock(CardTypeScriptable c)
    {
        locks.Add(c);
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
        GameObject go = GameObject.FindGameObjectWithTag("Enemy");
        currentEnemy = go.AddComponent<Enemy>();
        currentEnemy.SetMaxHealth(20);
        currentEnemy.SetHealth(20);
        isInBattle = true;
        currentEnemy.ChooseActions();
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
        PlayPlayerTurn();
    }

    private void PlayPlayerTurn()
    {
        DeckManager.Instance.DrawCards(3);
        Player.Instance.ReloadEnergy();
        ApplyDelayedEffects();
    }

    private void ApplyDelayedEffects()
    {
        while(queuedEffects.Count > 0)
        {
            CardEffect c = queuedEffects.Dequeue();
            c.DoEffect();
            c = null;
        }
    }
}
