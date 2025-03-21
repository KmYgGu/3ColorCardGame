using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 게임 시작할때 카드ui 오브젝트를 생성
// 카드를 얻거나 새로 뽑으면, 정보를 기반으로 해서 ui슬롯을 갱신
public class CardBoxUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform contentTrans;

    private List<UICardBoxCardSlot> slots = new List<UICardBoxCardSlot>();// 잘 나옴
    private UICardBoxCardSlot slot;

    private int currentCount;// 현재 몇개 만들었는지
    private int maxCount;   //몇 개까지 만들 수 있는지

    [SerializeField]private List<HaveCardStock> cardList; // 실제 유저가 가지고 있는 카드 목록    ..없음


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
        maxCount = CardDataManager.Inst.DICColorCardData.Count + CardDataManager.Inst.DICEventCardData.Count;//7;// 나중에 CardDataManager에서 총 카드의 갯수를 참조해야함
        for (int i = 0; i < maxCount; i++)
        {
            
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UICardBoxCardSlot>(out slot))
            {
                slot.SLOTINDEX = i;//1;
                slots.Add(slot);
            }
            else
                Debug.Log("cardBoxUI에서 InitSlot() 슬롯 생성 실패");
        }
        RefreshCardBoxUI();
    }

    // 현재 구현된 카드들을 참조해서 카드 슬롯을 갱신
    public void RefreshCardBoxUI()
    {

        cardList = GameManager.Inst.HCDATA.GetCardList();
        currentCount = GameManager.Inst.HCDATA.CurItemCount;


        for (int i = 0; i < maxCount; i++)
        {
            //slots[i].DrawCardSlot(cardList[i]);
            if (i < currentCount && cardList[i].cardID > -1)// 소유하고 있는 카드의 수가 현재 세고 있는 카드 수보다 많고, 그 카드 아이디가 0보다 클 때
            {
                slots[i].DrawCardSlot(cardList[i]);
            }
            else // 빈칸 슬롯
            {
                slots[i].ClearSlot();
            }

            //slots[i].SetSelectSlot(false); // 선택되지 않은 슬롯으로 오픈
        }
    }
}
