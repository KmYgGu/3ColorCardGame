using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCardStock//저장하지 않음
{
    public int cardID;  // 테이블 데이터의 ID
    //public int amount;
    //public int uID;     // 겹치지 않는 아이템의 고유 ID
}
public class AllCardData : SingleTon<AllCardData>//MonoBehaviour
{
    //private int maxItemSlot = CardDataManager.Inst.DICColorCardData.Count + CardDataManager.Inst.DICEventCardData.Count;

    [SerializeField]private List<AllCardStock> Allcard = new List<AllCardStock>();

    private void Start()
    {
        AddAllCard();
    }
    protected override void DoAwake()
    {
        //base.DoAwake();
        //AddAllCard();
    }
    // 습득하는 기능
    public void AddAllCard()
    {
        //int index = FindCardIndex(newCard);// 몇 번째 슬롯에 있는 카드인지 검사
        AllCardStock allcardstock = new AllCardStock();


        for (int i = 0; i < CardDataManager.Inst.DICColorCardData.Count; i++)
        {
            allcardstock.cardID = CardDataManager.Inst.ReturnColorCardTable(i).no;

            Allcard.Add(allcardstock);
        }
        for (int i = 0; i < CardDataManager.Inst.DICEventCardData.Count; i++)
        {
            allcardstock.cardID = CardDataManager.Inst.ReturnEventCardTable(i).no;

            Allcard.Add(allcardstock);
        }
        //Debug.Log(Allcard.Count);

    }
    // UI에 표기하기 위해서 외부에서 데이터를 참조
    public List<AllCardStock> GetCardList()
    {
        //CurItemCount = items.Count;
        return Allcard;
    }
    
    
}
