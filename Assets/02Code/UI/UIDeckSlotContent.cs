using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckSlotContent : MonoBehaviour
{
    // 나중에 PlayerData에 저장된 덱 매수 만큼 덱 버튼 슬롯을 만들어줘야함

    private RectTransform contentTrans;
    private UIDeckButton UIDeckBtn;

    [SerializeField] private GameObject DeckBtnPrefab;

    private void Awake()
    {
        TryGetComponent<RectTransform>(out contentTrans);

    }
    // 뷰포트 보다 콘텐츠사이즈가 커야 드래그가 된다
    public static Action OnButtonClicked;
    private void OnEnable()
    {
        OnButtonClicked += AddDeckButton;
    }
    private void OnDisable()
    {
        OnButtonClicked -= AddDeckButton;
    }

    void AddDeckButton()
    {



        if (Instantiate(DeckBtnPrefab, contentTrans).TryGetComponent<UIDeckButton>(out UIDeckBtn))
        {
            // 이 번호를 덱 최대 매수의 수치로 변경해야함
            UIDeckBtn.SLOTINDEX = 2;
            
        }
    }
}
