using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ī�带 �� �� ������ �ִ°�?
[System.Serializable]
public class DeckCardStock // ���� �ִ� ī�� �ϳ��� ����
{
    public int cardID;  // ���̺� �������� ID
    public int amount;
    public int uID;     // ��ġ�� �ʴ� �������� ���� ID
}

[System.Serializable]
public class DeckCardData
{
    private int maxDeckSlot = 20;
    public int MaxCounr => maxDeckSlot;

    private int curDeckSlot = 0;// �����ִ� ī�� ����
    public int CurDeckCount
    {
        get => curDeckSlot;
        set => curDeckSlot = value;
    }

    private List<DeckCardStock> Deckcards = new List<DeckCardStock>();


    // ���� �ִ� ���
    public void AddCard(DeckCardStock newCard)
    {
        int index = FindCardIndex(newCard);// �� ��° ���Կ� �ִ� ī������ �˻�

        // ���� �� ī�� ���� 20�� �̸����� Ȯ���ϰ�,

        // �÷� ī�� ���� �̺�Ʈ ī�� ���� ����

        // �÷� ī��� �ڵ����� 5�� ����, �� �÷� ī�� ������ ���ڸ��ų�, �־��� ��� 20���� �ʰ��ϴ� �� Ȯ��

        if (index < 0)// �κ��丮�� �Ȱ��� �������� ���� ���,
        {
            Deckcards.Add(newCard);
            //curDeckSlot++; // �̰� Deckui���� ó��

            //Debug.Log(newCard.cardID);
        }
        else
        {
            // ī������
            //curDeckSlot--;            
            Deckcards.RemoveAt(index);
            Debug.Log("������ �ش� ī�带 �����մϴ� ���� �� ���� �� :" + Deckcards.Count);
                       
            
        }

    }

    // UI�� ǥ���ϱ� ���ؼ� �ܺο��� �����͸� ����
    public List<DeckCardStock> GetCardList()
    {
        //CurDeckCount = Deckcards.Count;

        return Deckcards;
    }
    // ī�带 �����ϸ� ���� ��ȭ ȹ�� // �� ī��� �ʿ��ұ�?
    public int GetPieces(DeckCardStock deletecard)
    {
        int index = FindCardIndex(deletecard);// �� ��° ���Կ� �ִ� ī������ �˻�



        if (index < 0)   // ã�� ���� ��Ȳ
        {
            return -1;
        }
        else
        {
            if (Deckcards[index].amount < deletecard.amount)// ������ �ִ� ������ ������ ���� ������� �Ҷ�
            {
                return -2;
            }
            else
            {
                Deckcards[index].amount -= deletecard.amount;
                if (Deckcards[index].amount <= 0)
                {
                    Deckcards.RemoveAt(index);
                    curDeckSlot--;
                }
            }
        }
        return 0;   //���� ����

    }

    // �� �κ��丮�� �̹� �ִ� �������� ���� ������ ��ø
    private int FindCardIndex(DeckCardStock newCard)
    {
        for (int i = Deckcards.Count - 1; i >= 0; i--)
            if (Deckcards[i].cardID == newCard.cardID)// ����Ʈ�� �ִ� ī�� ���̵�� ã���ִ� ī�� ���̵� ����
            {
                return i;
            }
        return -1;
    }
}
