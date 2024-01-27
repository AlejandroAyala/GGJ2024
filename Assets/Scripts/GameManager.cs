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
    private List<CardScriptable> allCards = new List<CardScriptable>();

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
            if(currentDeck.Count > 0)
            {
                UIManager.Instance.InstantiateCard(currentDeck[0]);
                currentDeck.RemoveAt(0);
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
        currentDeck = currentDeck.OrderBy(x => random.Next()).ToList();
        random = null;
    }

    public void OrderDeck()
    {        
        currentDeck = currentDeck.OrderBy(x => x.name).ToList();
    }

    public void AddCardToDeck(CardScriptable card)
    {
        currentDeck.Add(card);
    }

    public void GenerateRandomDeck(int cardQuantity)
    {
        currentDeck.Clear();
        for(int i = 0; i<cardQuantity;i++)
        {
            currentDeck.Add(allCards[UnityEngine.Random.Range(0,allCards.Count-1)]);
        }
    }
}
