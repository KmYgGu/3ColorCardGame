using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 덱에 카드를 몇 개 가지고 있는가?
[System.Serializable]
public class DeckCardStock // 덱에 있는 카드 하나의 정보
{
    public int cardID;  // 테이블 데이터의 ID
    public int amount;
    public int uID;     // 겹치지 않는 아이템의 고유 ID

    public bool isColorCard; // 컬러 카드일 경우 true
    public bool ownisme;    // 해당 카드의 주인이 자신이 맞는지
}

[System.Serializable]
public class DeckCardData
{
    //private int maxDeckSlot;// = 20;
    //public int MaxCounr => maxDeckSlot;

    //private int curDeckSlot = 0;// 덱에있는 카드 갯수 추후 게임 매니저를 통해 초기화를 해야함
    /*public int CurDeckCount
    {
        get => curDeckSlot;
        set => curDeckSlot = value;
    }*/

    private List<DeckCardStock> Deckcards = new List<DeckCardStock>();

    public List<DeckCardStock> DECKcards
    {
        get => Deckcards;
    }

    public string DeckName;

    // 덱에 넣는 기능
    public void AddCard(DeckCardStock newCard)
    {
        int index = FindCardIndex(newCard);// 몇 번째 슬롯에 있는 카드인지 검사

        // 먼저 덱 카드 수가 20장 미만인지 확인하고(남아 있는 빈슬롯을 확인),

        // 컬러 카드 인지 이벤트 카드 인지 구별
        
        int looptime = 0;
        if (AllCardData.Inst.isColorCardTocardno(newCard.cardID))// 이 체크를 카드에 넣기 전에 확인해야함
        {
            // 5번 추가를 하면 덱이 20장을 넘어가는 지 확인(이미 생성된 슬롯보다 더많은 슬롯이 필요할 때)
            
            looptime = 5;
            //if (looptime + curDeckSlot > 20) return;

        }
        // 이벤트 카드이면 한장만 추가
        else
        {
            looptime = 1;
        }

        // 컬러 카드는 자동으로 5장 투입, 단 컬러 카드 갯수가 모자르거나, 넣었을 경우 20장을 초과하는 지 확인
        
        if (index < 0)// 인벤토리에 똑같은 아이템이 없는 경우,
        {
            for (int j = 0; j < looptime; j++)
            {
                

                Deckcards.Add(newCard);
                //curDeckSlot++; // 이건 Deckui에서 처리
            }

        }
        else// 같은 카드제거
        {

            /*curDeckSlot--;            
            Deckcards.RemoveAt(index);
            Debug.Log("덱에서 해당 카드를 제거합니다 현재 덱 매수 :" + Deckcards.Count);*/

            for (int j = 0; j < looptime; j++)
            {
                index = FindCardIndex(newCard);
                //curDeckSlot--;
                Deckcards.RemoveAt(index);
            }
            Debug.Log("덱에서 해당 카드를 제거합니다 현재 덱 매수 :" + Deckcards.Count);
        }             
    }

    //cPU덱 전용 추가는 중복확인 하지않고 바로 추가
    public void CPUDeckAddcard(DeckCardStock newCard)
    {
        Deckcards.Add(newCard);
    }

    // UI에 표기하기 위해서 외부에서 데이터를 참조
    public List<DeckCardStock> GetCardList()
    {
        //CurDeckCount = Deckcards.Count;

        return Deckcards;
    }

    // 카드를 분해하면 일정 재화 획득 // 덱 카드는 필요할까?
    public int GetPieces(DeckCardStock deletecard)
    {
        int index = FindCardIndex(deletecard);// 몇 번째 슬롯에 있는 카드인지 검사



        if (index < 0)   // 찾지 못한 상황
        {
            return -1;
        }
        else
        {
            if (Deckcards[index].amount < deletecard.amount)// 가지고 있는 수보다 더많은 수를 지울려고 할때
            {
                return -2;
            }
            else
            {
                Deckcards[index].amount -= deletecard.amount;
                if (Deckcards[index].amount <= 0)
                {
                    Deckcards.RemoveAt(index);
                    //curDeckSlot--;
                }
            }
        }
        return 0;   //삭제 성공

    }

    // 현 인벤토리에 이미 있는 아이템을 습득 했으면 중첩
    private int FindCardIndex(DeckCardStock newCard)
    {
        for (int i = Deckcards.Count - 1; i >= 0; i--)
            if (Deckcards[i].cardID == newCard.cardID)// 리스트에 있는 카드 아이디와 찾고있는 카드 아이디가 같다
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
