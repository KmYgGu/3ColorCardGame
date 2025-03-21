using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DcAtion : MonoBehaviour, IDeck
{
    DeckInfo deckInfo;
    HandCardInfo handCardInfo;

    private void Awake()
    {
        TryGetComponent<DeckInfo>(out deckInfo);
        TryGetComponent<HandCardInfo>(out handCardInfo);
    }

    private void Start()
    {
        //DrawCard();
    }

    public void CheckDeck()
    {
        if (deckInfo.DeckinCards != 20)
        {
            Debug.Log("덱 카드 매수가 20장이 아닙니다! (이건 덱 편집화면에서 우선)");
        } 
    }

    public void DrawCard()
    {
        Debug.Log("카드 드로우");
        deckInfo.DeckinCards--;

        handCardInfo.HandCardsindex++;

        Debug.Log($"현재 내 덱의 카드는 {deckInfo.DeckinCards}, 내 패는 {handCardInfo.HandCardsindex}");
        //throw new System.NotImplementedException();
    }

    public void FirstDraw()
    {
        throw new System.NotImplementedException();
    }

    public void ShowRemainCard()
    {
        throw new System.NotImplementedException();
    }

    public void ShuffleDeck()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    
}
