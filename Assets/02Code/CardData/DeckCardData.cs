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

    public bool isColorCard; // �÷� ī���� ��� true
    public bool ownisme;    // �ش� ī���� ������ �ڽ��� �´���
}

[System.Serializable]
public class DeckCardData
{
    private int maxDeckSlot;// = 20;
    public int MaxCounr => maxDeckSlot;

    private int curDeckSlot = 0;// �����ִ� ī�� ���� ���� ���� �Ŵ����� ���� �ʱ�ȭ�� �ؾ���
    public int CurDeckCount
    {
        get => curDeckSlot;
        set => curDeckSlot = value;
    }

    private List<DeckCardStock> Deckcards = new List<DeckCardStock>();

    public List<DeckCardStock> DECKcards
    {
        get => Deckcards;
    }

    private string DeckName;

    // ���� �ִ� ���
    public void AddCard(DeckCardStock newCard)
    {
        int index = FindCardIndex(newCard);// �� ��° ���Կ� �ִ� ī������ �˻�

        // ���� �� ī�� ���� 20�� �̸����� Ȯ���ϰ�(���� �ִ� �󽽷��� Ȯ��),

        // �÷� ī�� ���� �̺�Ʈ ī�� ���� ����
        
        int looptime = 0;
        if (AllCardData.Inst.isColorCardTocardno(newCard.cardID))// �� üũ�� ī�忡 �ֱ� ���� Ȯ���ؾ���
        {
            // 5�� �߰��� �ϸ� ���� 20���� �Ѿ�� �� Ȯ��(�̹� ������ ���Ժ��� ������ ������ �ʿ��� ��)
            
            looptime = 5;
            //if (looptime + curDeckSlot > 20) return;

        }
        // �̺�Ʈ ī���̸� ���常 �߰�
        else
        {
            looptime = 1;
        }

        // �÷� ī��� �ڵ����� 5�� ����, �� �÷� ī�� ������ ���ڸ��ų�, �־��� ��� 20���� �ʰ��ϴ� �� Ȯ��
        
        if (index < 0)// �κ��丮�� �Ȱ��� �������� ���� ���,
        {
            for (int j = 0; j < looptime; j++)
            {
                

                Deckcards.Add(newCard);
                curDeckSlot++; // �̰� Deckui���� ó��
            }

        }
        else// ���� ī������
        {

            /*curDeckSlot--;            
            Deckcards.RemoveAt(index);
            Debug.Log("������ �ش� ī�带 �����մϴ� ���� �� �ż� :" + Deckcards.Count);*/

            for (int j = 0; j < looptime; j++)
            {
                index = FindCardIndex(newCard);
                curDeckSlot--;
                Deckcards.RemoveAt(index);
            }
            Debug.Log("������ �ش� ī�带 �����մϴ� ���� �� �ż� :" + Deckcards.Count);
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

    public void SetDeckName(string name)
    {
        DeckName = name;
    }
}
