using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfo : MonoBehaviour, IWhosDeck
{
    GameObject owner;
    private int deckinCards; // 나중에 리스트로 변경해야함

    public int DeckinCards
    {
        get => deckinCards;
        set
        {

            deckinCards = value;
            PlayerDCCha?.Invoke();// 덱 카드수가 변경되면 델리게이트 실행
        }

    }
    public delegate void PlayerDeckinCardChanged();
    public event PlayerDeckinCardChanged PlayerDCCha;

    int DeckinColorCards;

    int DeckinEventCards;
       

    public void CheckDeck(GameObject who, int index)
    {
        owner = who;
        DeckinCards = index;
    }
 

    public void CheckDeckColorCard(int index)
    {
        DeckinColorCards = index;
    }

    public void CheckDeckEventCard(int index)
    {
        DeckinEventCards = index;
    }
}
