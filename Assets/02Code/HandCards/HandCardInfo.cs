using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardInfo : MonoBehaviour, IWhosHandCards
{
    GameObject owner;
    public int HandCardsindex; // 나중에 리스트로 수정해야함

    int HandColorCards;

    int HandEventCards;

    

    public void CheckHandCards(GameObject who, int index)// 전체 카드 갯수 체크
    {
        owner = who;
        HandCardsindex = index;
    }

    public void CheckHandColorCard(int index)// 색 카드 갯수 체크
    {
        HandColorCards = index;
    }

    public void CheckHandEventCard(int index)// 이벤크 카드 갯수 체크
    {
        HandEventCards = index;
    }

    
}
