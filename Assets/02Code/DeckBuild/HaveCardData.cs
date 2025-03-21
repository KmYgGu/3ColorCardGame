using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ư�� ī�带 �� �� ������ �ִ°�?
[System.Serializable]
public class HaveCardStock
{
    public int cardID;  // ���̺� �������� ID
    public int amount;
    public int uID;     // ��ġ�� �ʴ� �������� ���� ID
}

// ���̺� ��ü�� ����
[System.Serializable]
public class HaveCardData// : MonoBehaviour �����ؾ� ������ ��
{
    //private int maxItemSlot = 18;
    //public int MaxCounr => maxItemSlot;

    private int curItemSlot;// �������� ī�� ����
    public int CurItemCount
    {
        get => curItemSlot;
        set => curItemSlot = value;
    }

    private List<HaveCardStock> items = new List<HaveCardStock>();
    

    // �����ϴ� ���
    public void AddCard(HaveCardStock newCard)
    {
        int index = FindCardIndex(newCard);// �� ��° ���Կ� �ִ� ī������ �˻�

        //Debug.Log(index);

        //if (CardDataManager.Inst.GetColorCardData(newCard.cardID, out colorCardData_Entity ColorCardData))
        {
            
        }
        if (index < 0)// �κ��丮�� �Ȱ��� �������� ���� ���,
        {
            items.Add(newCard);
            curItemSlot++;
            //Debug.Log("HaveCardData���� �߰���");
        }
        else
        {
            items[index].amount += newCard.amount;
        }


    }
    // UI�� ǥ���ϱ� ���ؼ� �ܺο��� �����͸� ����
    public List<HaveCardStock> GetCardList()
    {
        CurItemCount = items.Count;
        return items;
    }
    // ī�带 �����ϸ� ���� ��ȭ ȹ��
    public int GetPieces(HaveCardStock deletecard)
    {
        int index = FindCardIndex(deletecard);// �� ��° ���Կ� �ִ� ī������ �˻�

        

        if(index < 0)   // ã�� ���� ��Ȳ
        {
            return -1;
        }
        else
        {
            if(items[index].amount < deletecard.amount)// ������ �ִ� ������ ������ ���� ������� �Ҷ�
            {
                return -2;
            }
            else
            {
                items[index].amount -= deletecard.amount;
                if (items[index].amount <= 0)
                {
                    items.RemoveAt(index);
                    curItemSlot--;
                }
            }
        }
        return 0;   //���� ����

    }

    // �� �κ��丮�� �̹� �ִ� �������� ���� ������ ��ø
    private int FindCardIndex(HaveCardStock newCard)
    {
        for (int i = items.Count - 1; i >= 0; i--)
            if(items[i].cardID == newCard.cardID)// ����Ʈ�� �ִ� ī�� ���̵�� ã���ִ� ī�� ���̵� ����
            {
                return i;
            }
        return -1;
    }
}
