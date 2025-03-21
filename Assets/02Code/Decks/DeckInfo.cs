using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInfo : MonoBehaviour, IWhosDeck
{
    GameObject owner;
    private int deckinCards; // ���߿� ����Ʈ�� �����ؾ���

    public int DeckinCards
    {
        get => deckinCards;
        set
        {

            deckinCards = value;
            PlayerDCCha?.Invoke();// �� ī����� ����Ǹ� ��������Ʈ ����
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
