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
            Debug.Log("�� ī�� �ż��� 20���� �ƴմϴ�! (�̰� �� ����ȭ�鿡�� �켱)");
        } 
    }

    public void DrawCard()
    {
        Debug.Log("ī�� ��ο�");
        deckInfo.DeckinCards--;

        handCardInfo.HandCardsindex++;

        Debug.Log($"���� �� ���� ī��� {deckInfo.DeckinCards}, �� �д� {handCardInfo.HandCardsindex}");
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
