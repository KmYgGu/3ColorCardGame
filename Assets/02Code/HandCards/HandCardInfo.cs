using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCardInfo : MonoBehaviour, IWhosHandCards
{
    GameObject owner;
    public int HandCardsindex; // ���߿� ����Ʈ�� �����ؾ���

    int HandColorCards;

    int HandEventCards;

    

    public void CheckHandCards(GameObject who, int index)// ��ü ī�� ���� üũ
    {
        owner = who;
        HandCardsindex = index;
    }

    public void CheckHandColorCard(int index)// �� ī�� ���� üũ
    {
        HandColorCards = index;
    }

    public void CheckHandEventCard(int index)// �̺�ũ ī�� ���� üũ
    {
        HandEventCards = index;
    }

    
}
