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

    public static Action<int> OnButtonClicked;

    private void OnEnable()
    {
        OnButtonClicked += RefreshCardBoxUI;
    }
    private void OnDisable()
    {
        OnButtonClicked -= RefreshCardBoxUI;
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
                //slot.EMPTY =
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

                GameManager.Inst.DCDATA.CurDeckCount--;  // 덱에 등록된 카드 수 감소
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
                    GameManager.Inst.DCDATA.CurDeckCount++;
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
        //Debug.Log(spaceslot);
        ReorderDeckUI();

    }

    private void ReorderDeckUI()
    {
        
        slots = slots.OrderBy(slot => slot.SLOTCARDNO == 0 ? int.MaxValue : slot.SLOTCARDNO).ToList();

        //  정렬된 슬롯 UI 갱신
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }
}
