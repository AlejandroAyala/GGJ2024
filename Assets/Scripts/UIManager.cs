using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public Canvas canvas;
    public GameObject cardPrefab;

    private void OnEnable()
    {
        canvas = GetComponent<Canvas>();
    }

    public void InstantiateCard(CardScriptable card)
    {
        GameObject c = Instantiate(cardPrefab, canvas.transform);
        c.GetComponent<Card>().SetCardInfo(card);
    }

}
