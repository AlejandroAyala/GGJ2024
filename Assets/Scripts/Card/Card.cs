using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Linq;

public class Card : MonoBehaviour
{
    [SerializeField]
    public CardScriptable cardInfo;
    [SerializeField]
    public TMPro.TextMeshProUGUI title;
    [SerializeField]
    public TMPro.TextMeshProUGUI desc;
    [SerializeField]
    public TMPro.TextMeshProUGUI effectText;
    [SerializeField]
    public TMPro.TextMeshProUGUI damage;
    [SerializeField]
    public TMPro.TextMeshProUGUI energy;
    [SerializeField]
    public UnityEngine.UI.Image cardImage;
    [SerializeField]
    public UnityEngine.UI.Image cardArt;
    public UnityEngine.UI.Button button;
    public Vector3 ogPos;
    public Vector3 target;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        gameObject.SetActive(false);
    }

    public void SetCardInfo(CardScriptable card)
    {
        cardInfo = card;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(PlayCard);
        title.text = card.cardName;
        desc.text = card.cardDescriptions[Random.Range(0, card.cardDescriptions.Length-1)];
        StringBuilder sb = new StringBuilder();
        foreach(CardEffect c in card.cardEffects)
        {
            sb.Append(c.description);
            sb.AppendLine();
        }
        effectText.text = sb.ToString();
        CardEffect damageEffect = card.cardEffects.Where((effect) => { return effect.type == CardType.DAMAGE; }).FirstOrDefault();
        if(damageEffect != null)
        {
            damage.text = damageEffect.applyAmmount.ToString();
            damage.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            damage.transform.parent.gameObject.SetActive(false);
        }
        energy.text = card.energyCost.ToString();
        cardImage.sprite = card.cardImage;
        cardArt.color = card.cardType.cardColor;
        gameObject.SetActive(true);
    }

    public void PlayCard()
    {
        //check locks
        if(!GameManager.Instance.GetLock(cardInfo.cardType))
        {
            if (Player.Instance.energy >= cardInfo.energyCost)
            {
				Player.Instance.animator.SetTrigger(cardInfo.animation_trigger);
				Player.Instance.energy -= cardInfo.energyCost;
                foreach (CardEffect ce in cardInfo.cardEffects)
                {
                    ce.DoEffect();
                }
                DeckManager.Instance.RemoveFromHand(this);
                //play animation
                if (cardInfo.cardType.typeName != "Garbage")
                {
                    DeckManager.Instance.AddToDiscardPile(cardInfo);
                }
                UIManager.Instance.ReturnCardToQueue(this);
            }
        }
    }

    public void MoveCard()
    {
        ogPos = transform.position;
        transform.position = new Vector3(ogPos.x, ogPos.y, ogPos.z)+(Vector3.up*300f);
    }

    public void ResetCardPosition()
    {
        transform.position = ogPos;
    }


}
