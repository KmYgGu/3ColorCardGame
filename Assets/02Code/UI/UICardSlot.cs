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

    private int slotIndex;
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    protected Image selectedImg;
    private Image cardimg;
    //public Button button;
    private Transform btnTrans;
    [SerializeField] private TextMeshProUGUI amount;

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
    public virtual void DrawCardSlot(HaveCardStock havecardData)//데이터 매니저에서 가져오기
    {
        if (havecardData.cardID % 10 == 0)// 카드의 고유 번호 일의 자리가 0이면 컬러 카드
        {
            if (CardDataManager.Inst.GetColorCardData(havecardData.cardID, out colorCardData_Entity cardInfo))
            {
                //Debug.Log(cardInfo.cardicon);
                // 동적 로딩을 통해 icon의 변경

                cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                cardimg.enabled = true;
                ChangeAmount(havecardData.amount);  // 보류갯수 갱신
                isEmpty = false; // 비어있지 않으니 false
            }
            else
            {
                Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {havecardData.cardID}");
            }
        }
        else if (havecardData.cardID % 10 == 1)// 카드의 고유 번호 일의 자리가 1이면 이벤트 카드
        {
            if (CardDataManager.Inst.GetEventCardData(havecardData.cardID, out eventCardData_Entity cardInfo))
            {

                cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// 테이블에 적힌 파일경로를 기반으로 동적 로딩
                cardimg.enabled = true;
                ChangeAmount(havecardData.amount);  // 보류갯수 갱신
                isEmpty = false; // 비어있지 않으니 false
            }
            else
            {
                Debug.Log($"UICardSlot. 테이블에 없는 카드 입니다. {havecardData.cardID}");
            }
        }

    }

    private  void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public virtual void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        //cardimg.enabled = false;
        Color imgColor = cardimg.color;
        imgColor.a = 0.5f;
        cardimg.color = imgColor;
        amount.enabled = false;

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
