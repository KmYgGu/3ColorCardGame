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

    private void Start()
    {
        //AddAllCard();
    }
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
            //Debug.Log(allcardstock.cardID + "하나");
            Allcard.Add(allcardstock);
        }
        for (int i = 0; i < cardList.eventCardData.Count; i++)
        {
            AllCardStock allcardstock = new AllCardStock();
            allcardstock.cardID = cardList.eventCardData[i].no;
            //Debug.Log(allcardstock.cardID + "둘");
            Allcard.Add(allcardstock);
        }

        

    }
    // UI에 표기하기 위해서 외부에서 데이터를 참조
    public List<AllCardStock> GetCardList()
    {

        return Allcard;
    }
    
    
}
