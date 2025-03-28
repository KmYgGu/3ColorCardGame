using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCardStock//저장하지 않음
{
    public int cardID;  // 테이블 데이터의 ID
    
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
    // 습득하는 기능
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
    // UI에 표기하기 위해서 외부에서 데이터를 참조
    public List<AllCardStock> GetCardList()
    {

        return Allcard;
    }

    // 정수값을 받으면 해당 정수값에 해당되는 카드가 컬러 카드인지
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
    // 카드 번호값을 받아서 그 카드가 컬러 카드인지
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
