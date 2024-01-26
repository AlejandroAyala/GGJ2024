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

    public void SetCardInfo(CardScriptable card)
    {
        cardInfo = card;
        title.text = card.name;
        desc.text = card.cardDescriptions[Random.Range(0, card.cardDescriptions.Length-1)];
        StringBuilder sb = new StringBuilder();
        foreach(CardEffect c in card.cardEffects)
        {
            sb.Append(c.description);
            sb.AppendLine();
        }
        effectText.text = sb.ToString();
        CardEffect damageEffect = card.cardEffects.Where((effect) => { return effect.type == Type.DAMAGE; }).FirstOrDefault();
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
    }


}
