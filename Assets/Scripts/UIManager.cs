using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Canvas canvas;
    public GameObject cardPrefab;
    public Queue<Card> cardPool = new Queue<Card>();

    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();

    }

    public Card InstantiateCard(CardScriptable card)
    {
        Card c = null;
        if (cardPool.Count != 0)
        {
            c = Instantiate(cardPrefab, canvas.transform).GetComponent<Card>();
            c.GetComponent<Card>().SetCardInfo(card);
        }
        else
        {
            c = cardPool.Dequeue();
            c.SetCardInfo(card);
        }
        return c;
    }

    public void ReturnCardToQueue(Card card)
    {
        card.gameObject.SetActive(false);
        cardPool.Enqueue(card);
    }

}
