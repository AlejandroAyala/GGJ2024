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

    public Coroutine currentC;

    private void Awake()
    {
        button = GetComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(PlayCard);
        gameObject.SetActive(false);
    }

    public void SetCardInfo(CardScriptable card)
    {
        cardInfo = card;
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
        foreach(CardEffect ce in cardInfo.cardEffects)
        {
            ce.DoEffect();
        }
        DeckManager.Instance.RemoveFromHand(this);
        //play animation
        DeckManager.Instance.AddToDiscardPile(cardInfo);
        UIManager.Instance.ReturnCardToQueue(this);
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
