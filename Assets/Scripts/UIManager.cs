using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Canvas canvas; 
    public GameObject panel;
    public GameObject cardPrefab;
    public Queue<Card> cardPool = new Queue<Card>();
    public TMPro.TextMeshProUGUI energy;
    public TMPro.TextMeshProUGUI deckAmmount;
    public TMPro.TextMeshProUGUI discardAmmount;
    public Slider HP;
    public Button nexTurn;

    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();
        nexTurn.onClick.AddListener(GameManager.Instance.EndPlayerTurn);
    }

    public void SetHP(int value)
    {
        if(this.HP!=null)
        {
            this.HP.value = value;
        }
    }

    public void SetMaxHP(int value)
    {
        if(this.HP!=null)
        {
            this.HP.maxValue = value;
        }
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

    public void SetDiscardAmmount(int discardAmmount)
    {
        if (this.discardAmmount != null)
        {
            this.discardAmmount.text = discardAmmount.ToString();
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
