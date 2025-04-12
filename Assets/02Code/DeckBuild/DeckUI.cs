using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

// 게임 시작할때 카드ui 오브젝트를 생성
// 카드를 얻거나 새로 뽑으면, 정보를 기반으로 해서 ui슬롯을 갱신
public class DeckUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform contentTrans;

    private List<UIDeckCardSlot> slots = new List<UIDeckCardSlot>();
    private UIDeckCardSlot slot;
        
    private int maxCount;   //몇 개까지 만들 수 있는지
    private int spaceslot; // 비어있는 공간을 표시

    private List<AllCardStock> cardList; // 모든 카드 데이터

    public static Action<int> OnButtonClicked; //UICardBoxCardSlot에서 버튼을 눌렀을 때
    public static Action OnDeckbtnClicked;

    private void OnEnable()
    {
        OnButtonClicked += RefreshCardBoxUI;
        OnDeckbtnClicked += RefreshCardBoxUI_FromDeckData;
    }
    private void OnDisable()
    {
        OnButtonClicked -= RefreshCardBoxUI;
        OnDeckbtnClicked -= RefreshCardBoxUI_FromDeckData;
    }

    private void Awake()
    {
        //InitSlot();
    }
    private void Start()
    {
        InitSlot();
    }
    // 카드 슬롯을 생성하고 초기화
    private void InitSlot()
    {
        
        maxCount = 20;// 나중에 CardDataManager에서 총 카드의 갯수를 참조해야함
        spaceslot = maxCount;

        for (int i = 0; i < maxCount; i++)
        {
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UIDeckCardSlot>(out slot))
            {
                slot.SLOTINDEX = i;
                slots.Add(slot);
            }
            else
                Debug.Log("DeckUI에서 InitSlot() 슬롯 생성 실패");
        }

        //RefreshCardBoxUI();
    }

    // 현재 구현된 카드들을 참조해서 카드 슬롯을 갱신
    public void RefreshCardBoxUI(int index)
    {
        // 받은 ui버튼의 번호를 통해 카드의 넘버를 가져오기
        int slotcardno;
        if(index < CardDataManager.Inst.DICColorCardData.Count)
        {
            slotcardno = CardDataManager.Inst.ReturnColorCardTable(index).no;
        }
        else
        {
            slotcardno = CardDataManager.Inst.ReturnEventCardTable(index - CardDataManager.Inst.DICColorCardData.Count).no;
        }

        cardList = AllCardData.Inst.GetCardList();
        
      
        bool cardFound = false;
        

        // 이미 생성된 슬롯들에서 해당 카드 번호를 가진 슬롯이 있는지 확인
        for (int i = 0; i < slots.Count; i++)
        {
            // slot의 cardID가 cardNumber와 같다면 해당 슬롯에 카드가 있다는 의미
            if (slots[i].SLOTCARDNO == slotcardno)
            {
                slots[i].ClearSlot();  // 슬롯 클리어: 카드 삭제
                //slots.RemoveAt(i);

                //GameManager.Inst.DCDATA.CurDeckCount--;  // 덱에 등록된 카드 수 감소

                spaceslot++;
                //Debug.Log("해당 카드 삭제!");
                cardFound = true;

                //break;  // 한 슬롯만 삭제하면 된다면 break; 여러 개면 제거 처리 반복
            }

        }

        // 해당 카드 번호를 가진 슬롯이 없으면 추가
        if (!cardFound)
        {
            int looptime;// = 0;

            // 만약 받은 카드가 컬러 카드인지 이벤트 카드인지 확인
            // 컬러 카드이면 5장 추가
            if (AllCardData.Inst.isColorCard(index))// 이 체크를 카드에 넣기 전에 확인해야함
            {
                // 5번 추가를 하면 덱이 20장을 넘어가는 지 확인(이미 생성된 슬롯보다 더많은 슬롯이 필요할 때)
                looptime = 5;
                if(looptime > spaceslot)
                {
                    Debug.Log("빈 슬롯이 넣게 될 카드수 보다 적습니다");
                    return;
                }
            }
            // 이벤트 카드이면 한장만 추가
            else
            {
                looptime = 1;
            }

            for (int j = 0; j < looptime; j++)
            {
                // 빈 슬롯 찾기
                int emptyIndex = -1;
                for (int i = 0; i < slots.Count; i++)
                {

                    if (slots[i].SLOTCARDNO < 1) // 슬롯이 비어있다면
                    {
                        emptyIndex = i;
                        break;
                    }
                }
                if (emptyIndex != -1)
                {
                    slots[emptyIndex].DrawCardSlot(cardList[index]);

                    //GameManager.Inst.DCDATA.CurDeckCount++;

                    spaceslot--;
                    //Debug.Log("카드 추가!");
                    /*if (cardList[emptyIndex].cardID > -1)
                    {

                        slots[emptyIndex].DrawCardSlot(cardList[index]);
                        GameManager.Inst.DCDATA.CurDeckCount++;
                        Debug.Log("카드 추가!");
                    }
                    else
                    {
                        Debug.Log("유효하지 않은 카드입니다.");
                    }*/
                }
                else
                {
                    Debug.Log("슬롯이 가득 찼습니다.");
                }
            }
          

        }
        ReorderDeckUI();

    }

    // 카드 번호에 따라 정렬
    private void ReorderDeckUI()
    {
        
        slots = slots.OrderBy(slot => slot.SLOTCARDNO == 0 ? int.MaxValue : slot.SLOTCARDNO).ToList();

        //  정렬된 슬롯 UI 갱신
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }

    public void RefreshCardBoxUI_FromDeckData()
    {
        /*cardList = AllCardData.Inst.GetCardList();
        List<DeckCardStock> deckCards = GameManager.Inst.DCDATA.GetCardList();//DECKcards;
        int maxDeckSlot = GameManager.Inst.DCDATA.CurDeckCount; // 덱의 최대 슬롯 개수
                
        int deckSize = deckCards.Count; // 현재 덱에 있는 카드 개수

        // 기존 슬롯을 덱의 카드 정보로 갱신
        for (int i = 0; i < slots.Count; i++)//maxDeckSlot
        {
            if (i < deckSize) // 덱에 있는 카드 정보로 UI 갱신
            {
                //DeckCardStock cardData = deckCards[i];
                int cardID = deckCards[i].cardID;

                if (cardID > 0 && cardID < cardList.Count) // 유효한 카드 ID인지 확인
                {
                    slots[i].DrawCardSlot(deckCards[i]); // 해당 슬롯에 카드 배치
                    Debug.Log($"{i}번째 이 카드는 {deckCards[i].cardID}");
                }
                else
                {
                    slots[i].ClearSlot(); // 카드가 유효하지 않다면 슬롯 초기화
                }
            }// 남은 카드들
            else
            {
                slots[i].ClearSlot(); // 덱 크기를 초과하는 슬롯 초기화
            }
        }

        // 빈 슬롯 개수 업데이트
        //spaceslot = slots.Count - deckSize;
        spaceslot = slots.Count - maxDeckSlot;*/


        // 모든 카드 정보를 가져옴
        cardList = AllCardData.Inst.GetCardList();

        // 덱에 들어있는 카드 정보를 가져옴
        List<DeckCardStock> deckCards = GameManager.Inst.DCDATA.GetCardList();
        int deckSize = deckCards.Count;  // 현재 덱에 있는 카드 수
        //Debug.Log(deckSize);

        // UI 슬롯 전체를 한 번 Clear 처리 (이전 카드 정보 초기화)
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].ClearSlot();
        }

        // 덱 데이터에 맞게 슬롯에 카드 정보를 반영
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < deckSize) // 덱에 카드가 있는 슬롯에는
            {
                int cardID = deckCards[i].cardID;
                // 유효한 카드 ID이면
                if (cardID > 0)// && cardID < cardList.Count
                {
                    slots[i].DrawCardSlot(deckCards[i]);
                    Debug.Log($"{i}번째 슬롯에 카드 {deckCards[i].cardID} 적용");
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
            else
            {
                // 덱에 없는 슬롯은 명시적으로 Clear
                slots[i].ClearSlot();
            }
        }

        // 빈 슬롯 개수 업데이트: 전체 슬롯 수 - 덱에 들어있는 카드 수
        spaceslot = slots.Count - deckSize;


        // UI 정렬
        ReorderDeckUI();
    }

}
