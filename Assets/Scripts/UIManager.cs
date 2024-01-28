using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Canvas canvas; 
    public GameObject panel;
    public GameObject cardPrefab;
    public Queue<Card> cardPool = new Queue<Card>();
    public TMPro.TextMeshProUGUI energy;
    public TMPro.TextMeshProUGUI deckAmmount;

    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();
    }

    public void SetEnergy(int energy)
    {
        if(this.energy!=null)
        {
            this.energy.text = energy.ToString();
        }
    }

    public void SetDeckAmmount(int deckAmmount)
    {
        if (this.deckAmmount != null)
        {
            this.deckAmmount.text = deckAmmount.ToString();
        }
    }

    public Card InstantiateCard(CardScriptable card)
    {
        Card c = null;
        if (cardPool.Count == 0)
        {
            c = Instantiate(cardPrefab, panel.transform).GetComponent<Card>();
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
        card.button.onClick.RemoveAllListeners();
        cardPool.Enqueue(card);
    }

}
