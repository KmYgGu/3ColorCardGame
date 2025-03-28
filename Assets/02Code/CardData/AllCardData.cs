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

    private void Start()
    {
        //AddAllCard();
    }
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
            //Debug.Log(allcardstock.cardID + "�ϳ�");
            Allcard.Add(allcardstock);
        }
        for (int i = 0; i < cardList.eventCardData.Count; i++)
        {
            AllCardStock allcardstock = new AllCardStock();
            allcardstock.cardID = cardList.eventCardData[i].no;
            //Debug.Log(allcardstock.cardID + "��");
            Allcard.Add(allcardstock);
        }

        

    }
    // UI�� ǥ���ϱ� ���ؼ� �ܺο��� �����͸� ����
    public List<AllCardStock> GetCardList()
    {

        return Allcard;
    }
    
    
}
