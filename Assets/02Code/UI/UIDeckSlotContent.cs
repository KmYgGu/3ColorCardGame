using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckSlotContent : MonoBehaviour
{
    // ���߿� PlayerData�� ����� �� �ż� ��ŭ �� ��ư ������ ����������

    private RectTransform contentTrans;
    private UIDeckButton UIDeckBtn;

    [SerializeField] private GameObject DeckBtnPrefab;

    private void Awake()
    {
        TryGetComponent<RectTransform>(out contentTrans);

    }
    // ����Ʈ ���� ����������� Ŀ�� �巡�װ� �ȴ�
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
            // �� ��ȣ�� �� �ִ� �ż��� ��ġ�� �����ؾ���
            UIDeckBtn.SLOTINDEX = 2;
            
        }
    }
}
