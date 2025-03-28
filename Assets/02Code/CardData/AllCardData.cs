using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCardStock//�������� ����
{
    public int cardID;  // ���̺� �������� ID
    
}
public class AllCardData : SingleTon<AllCardData>//MonoBehaviour
{
    
    private List<AllCardStock> Allcard = new List<AllCardStock>();

    CardList cardList;

    protected override void DoAwake()
    {
        base.DoAwake();
        AddAllCard();
    }
    // �����ϴ� ���
    public void AddAllCard()
    {

        cardList = Resources.Load<CardList>("CardList");

        for (int i = 0; i < cardList.colorCardData.Count; i++)
        {
            AllCardStock allcardstock = new AllCardStock();
            allcardstock.cardID = cardList.colorCardData[i].no;
            
            Allcard.Add(allcardstock);
        }
        for (int i = 0; i < cardList.eventCardData.Count; i++)
        {
            AllCardStock allcardstock = new AllCardStock();
            allcardstock.cardID = cardList.eventCardData[i].no;
            
            Allcard.Add(allcardstock);
        }

        

    }
    // UI�� ǥ���ϱ� ���ؼ� �ܺο��� �����͸� ����
    public List<AllCardStock> GetCardList()
    {

        return Allcard;
    }

    // �������� ������ �ش� �������� �ش�Ǵ� ī�尡 �÷� ī������
    public bool isColorCard(int index)
    {
        if(Allcard[index].cardID % 10 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }


    }
    // ī�� ��ȣ���� �޾Ƽ� �� ī�尡 �÷� ī������
    public bool isColorCardTocardno(int index)
    {
        if(index % 10 == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
