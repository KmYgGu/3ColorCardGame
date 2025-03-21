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

    private List<UICardSlot> slots = new List<UICardSlot>();// 나중에 다시 고쳐야함
    private UICardSlot slot;

    private int currentCount;// 현재 몇개 만들었는지
    private int maxCount;   //몇 개까지 만들 수 있는지

    private List<HaveCardStock> cardList; // 실제 유저가 가지고 있는 카드 목록


    private void Awake()
    {
        InitSlot();
    }
    // 카드 슬롯을 생성하고 ㅊ토기화
    private void InitSlot()
    {
        maxCount = 20;// 나중에 CardDataManager에서 총 카드의 갯수를 참조해야함
        for (int i = 0; i < maxCount; i++)
        {
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UICardSlot>(out slot))//   생성함과 동시에 그 오브젝트의 컴포넌트 가져오기
            {
                slot.SLOTINDEX = 1;
                slots.Add(slot);
            }
            else
                Debug.Log("cardBoxUI에서 InitSlot() 슬롯 생성 실패");
        }
    }

    // 현재 구현된 카드들을 참조해서 카드 슬롯을 갱신
    public void RefreshCardBoxUI()
    {
        cardList = GameManager.Inst.HCDATA.GetCardList();
        currentCount = GameManager.Inst.HCDATA.CurItemCount;
        //maxCount = GameManager.Inst.HCDATA

        for (int i = 0; i < maxCount; i++)
        {
            if (i < currentCount && cardList[i].cardID > -1)
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
