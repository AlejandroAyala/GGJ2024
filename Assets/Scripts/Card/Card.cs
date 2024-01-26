using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        desc.text = card.cardDescription[0];
    }


}
