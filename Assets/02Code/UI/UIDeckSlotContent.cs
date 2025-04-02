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

    private void Start()
    {
        makeStartHaveDeck();
    }

    void AddDeckButton()
    {



        if (Instantiate(DeckBtnPrefab, contentTrans).TryGetComponent<UIDeckButton>(out UIDeckBtn))
        {
            // �� ��ȣ�� �� �ִ� �ż��� ��ġ�� �����ؾ���
            UIDeckBtn.SLOTINDEX = GameManager.Inst.PData.Decks;

            // �� �ż� �߰�
            GameManager.Inst.PData.Decks++;
        }
    }

    void makeStartHaveDeck()//������ �ִ� �� ���� ��ŭ ��ư�� �̸� ����
    {
        for(int i = 0; i < GameManager.Inst.PData.Decks; i++)
        {
            //Debug.Log(i);
            if (Instantiate(DeckBtnPrefab, contentTrans).TryGetComponent<UIDeckButton>(out UIDeckBtn))
            {
                // �� ��ȣ�� �� �ִ� �ż��� ��ġ�� �����ؾ���
                UIDeckBtn.SLOTINDEX = i;

                if(i != 0)
                {
                    //GameManager���� �� ���� �߰� �Լ� ����
                    GameManager.Inst.AddDeckSlot();
                }


                // ���Ŀ� ���ӸŴ������� �� �̸��� �����ؾ���

            }
        }
    }
}
