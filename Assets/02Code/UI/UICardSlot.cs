using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UICardSlot : MonoBehaviour
{
    private bool isEmpty;
    public bool EMPTY
    {
        get => isEmpty;
    }

    private int slotIndex;// 이값을 통해 버튼 자신이 몇 번째 버튼인지 알기
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    private int slotcardno;
    public int SLOTCARDNO// 이 버튼이 몇 번의 카드를 가지고 있는지
    {
        get => slotcardno;
        set => slotcardno = value;
    }

    protected Image selectedImg;
    private Image cardimg;
    //public Button button;
    private Transform btnTrans;
    private TextMeshProUGUI amount;

    private bool isSelect;
    public bool ISSelect
    {
        get => isSelect;
        set => isSelect = value;
    }
   

    protected virtual void Awake()
    {
        transform.GetChild(0).TryGetComponent<Image>(out selectedImg);
        transform.GetChild(1).TryGetComponent<Image>(out cardimg);

        transform.GetChild(1).TryGetComponent<Transform>(out btnTrans);
        btnTrans.GetChild(0).TryGetComponent<TextMeshProUGUI>(out amount);// 다른 UI 요소들은 RayCast 타겟을 꺼버리면 됨
                
    }
    //카드 정보 갱신
    public virtual void DrawCardSlot<T>(T havecardData)//데이터 매니저에서 가져오기
    {
        isSelect = true;
        //cardimg.enabled = false;
        Color imgColor = cardimg.color;
        imgColor.a = 1f;
        cardimg.color = imgColor;
        amount.enabled = false;

        if (havecardData is HaveCardStock cardStock)
        {
            if (cardStock.cardID % 10 == 0)// 카드의 고유 번호 일의 자리가 0이면 컬러 카드
            {
                if (CardDataManager.Inst.GetColorCardData(cardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // 동적 로딩을 통해 icon의 변경
                    slotcardno = cardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(cardStock.amount);  // 보류갯수 갱신
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {cardStock.cardID}");
                }
            }
            else if (cardStock.cardID % 10 == 1)// 카드의 고유 번호 일의 자리가 1이면 이벤트 카드
            {
                if (CardDataManager.Inst.GetEventCardData(cardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = cardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(cardStock.amount);  // 보유갯수 갱신
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {cardStock.cardID}");
                }
            }
        }
        else if (havecardData is AllCardStock allCardStock)
        {
            if (allCardStock.cardID % 10 == 0)// 카드의 고유 번호 일의 자리가 0이면 컬러 카드
            {
                if (CardDataManager.Inst.GetColorCardData(allCardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // 동적 로딩을 통해 icon의 변경
                    slotcardno = allCardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(0);  // 보류갯수 갱신
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {allCardStock.cardID}");
                }
            }
            else if (allCardStock.cardID % 10 == 1)// 카드의 고유 번호 일의 자리가 1이면 이벤트 카드
            {
                if (CardDataManager.Inst.GetEventCardData(allCardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = allCardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(0);  
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {allCardStock.cardID}");
                }
            }
        }

        else if (havecardData is DeckCardStock DeckCardStock)
        {
            if (DeckCardStock.cardID % 10 == 0)// 카드의 고유 번호 일의 자리가 0이면 컬러 카드
            {
                if (CardDataManager.Inst.GetColorCardData(DeckCardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // 동적 로딩을 통해 icon의 변경
                    slotcardno = DeckCardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(0);  // 보류갯수 갱신
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {DeckCardStock.cardID}");
                }
            }
            else if (DeckCardStock.cardID % 10 == 1)// 카드의 고유 번호 일의 자리가 1이면 이벤트 카드
            {
                if (CardDataManager.Inst.GetEventCardData(DeckCardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = DeckCardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                    cardimg.enabled = true;
                    ChangeAmount(0);
                    isEmpty = false; // 비어있지 않으니 false
                }
                else
                {
                    Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {DeckCardStock.cardID}");
                }
            }
        }

    }
        

    private void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public virtual void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        //cardimg.enabled = false;
        cardimg.sprite = null;
        Color imgColor = cardimg.color;
        imgColor.a = 0.5f;
        cardimg.color = imgColor;
        amount.enabled = false;
        slotcardno = 0;
        //gameObject.SetActive(false);
    }

    // 보유 갯수를 갱신
    public virtual void ChangeAmount(int newAnount)
    {
        if (newAnount < 2)
            amount.enabled = false;
        else
        {
            amount.enabled = true;
            amount.text = newAnount.ToString();
        }
    }

    
}
