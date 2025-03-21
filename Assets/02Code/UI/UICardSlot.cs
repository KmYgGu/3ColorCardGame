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

    private Image selectedImg;
    private Image cardimg;
    public Button button;
    private bool isSelect;

    private TextMeshPro amount;

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent<Image>(out selectedImg);
        transform.GetChild(1).TryGetComponent<Image>(out cardimg);
        transform.GetChild(2).TryGetComponent<TextMeshPro>(out amount);

        if (transform.GetChild(1).TryGetComponent<Button>(out button))
            button.onClick.AddListener(OnClick_Select);

    }
    //카드 정보 갱신
    public void DrawCardSlot(HaveCardStock havecardData)//데이터 매니저에서 가져오기
    {
        // 컬러카드 인경우
        if(CardDataManager.Inst.GetColorCardData(havecardData.cardID, out colorCardData_Entity cardInfo))
        {
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

    public void OnClick_Select()//추후에 수정
    {
        if (!isEmpty)
        {
            Debug.Log("카드를 클릭함");
            isSelect = !isSelect;
            selectedImg.enabled = isSelect;
        }
        
    }

    private void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        cardimg.enabled = false;
        amount.enabled = false;

        gameObject.SetActive(false);
    }

    // 보유 갯수를 갱신
    public void ChangeAmount(int newAnount)
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
