using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 특정 카드를 몇 개 가지고 있는가?
[System.Serializable]
public class HaveCardStock
{
    public int cardID;  // 테이블 데이터의 ID
    public int amount;
    public int uID;     // 겹치지 않는 아이템의 고유 ID
}

// 세이브 자체로 저장
[System.Serializable]
public class HaveCardData// : MonoBehaviour 제거해야 저장이 됨
{
    //private int maxItemSlot = 18;
    //public int MaxCounr => maxItemSlot;

    private int curItemSlot;// 소유중인 카드 갯수
    public int CurItemCount
    {
        get => curItemSlot;
        set => curItemSlot = value;
    }

    private List<HaveCardStock> items = new List<HaveCardStock>();
    

    // 습득하는 기능
    public void AddCard(HaveCardStock newCard)
    {
        int index = FindCardIndex(newCard);// 몇 번째 슬롯에 있는 카드인지 검사

        //Debug.Log(index);

        //if (CardDataManager.Inst.GetColorCardData(newCard.cardID, out colorCardData_Entity ColorCardData))
        {
            
        }
        if (index < 0)// 인벤토리에 똑같은 아이템이 없는 경우,
        {
            items.Add(newCard);
            curItemSlot++;
            //Debug.Log("HaveCardData에서 추가됨");
        }
        else
        {
            items[index].amount += newCard.amount;
        }


    }
    // UI에 표기하기 위해서 외부에서 데이터를 참조
    public List<HaveCardStock> GetCardList()
    {
        CurItemCount = items.Count;
        return items;
    }
    // 카드를 분해하면 일정 재화 획득
    public int GetPieces(HaveCardStock deletecard)
    {
        int index = FindCardIndex(deletecard);// 몇 번째 슬롯에 있는 카드인지 검사

        

        if(index < 0)   // 찾지 못한 상황
        {
            return -1;
        }
        else
        {
            if(items[index].amount < deletecard.amount)// 가지고 있는 수보다 더많은 수를 지울려고 할때
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
        return 0;   //삭제 성공

    }

    // 현 인벤토리에 이미 있는 아이템을 습득 했으면 중첩
    private int FindCardIndex(HaveCardStock newCard)
    {
        for (int i = items.Count - 1; i >= 0; i--)
            if(items[i].cardID == newCard.cardID)// 리스트에 있는 카드 아이디와 찾고있는 카드 아이디가 같다
            {
                return i;
            }
        return -1;
    }
}
