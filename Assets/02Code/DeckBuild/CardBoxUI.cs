using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �����Ҷ� ī��ui ������Ʈ�� ����
// ī�带 ��ų� ���� ������, ������ ������� �ؼ� ui������ ����
public class CardBoxUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform contentTrans;

    private List<UICardBoxCardSlot> slots = new List<UICardBoxCardSlot>();// �� ����
    private UICardBoxCardSlot slot;

    private int currentCount;// ���� � ���������
    private int maxCount;   //�� ������ ���� �� �ִ���

    [SerializeField]private List<HaveCardStock> cardList; // ���� ������ ������ �ִ� ī�� ���    ..����


    private void Awake()
    {
        //InitSlot();
    }
    private void Start()
    {
        InitSlot();
    }
    // ī�� ������ �����ϰ� �ʱ�ȭ
    private void InitSlot()
    {
        maxCount = CardDataManager.Inst.DICColorCardData.Count + CardDataManager.Inst.DICEventCardData.Count;//7;// ���߿� CardDataManager���� �� ī���� ������ �����ؾ���
        for (int i = 0; i < maxCount; i++)
        {
            
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UICardBoxCardSlot>(out slot))
            {
                slot.SLOTINDEX = i;//1;
                slots.Add(slot);
            }
            else
                Debug.Log("cardBoxUI���� InitSlot() ���� ���� ����");
        }
        RefreshCardBoxUI();
    }

    // ���� ������ ī����� �����ؼ� ī�� ������ ����
    public void RefreshCardBoxUI()
    {

        cardList = GameManager.Inst.HCDATA.GetCardList();
        currentCount = GameManager.Inst.HCDATA.CurItemCount;


        for (int i = 0; i < maxCount; i++)
        {
            //slots[i].DrawCardSlot(cardList[i]);
            if (i < currentCount && cardList[i].cardID > -1)// �����ϰ� �ִ� ī���� ���� ���� ���� �ִ� ī�� ������ ����, �� ī�� ���̵� 0���� Ŭ ��
            {
                slots[i].DrawCardSlot(cardList[i]);
            }
            else // ��ĭ ����
            {
                slots[i].ClearSlot();
            }

            //slots[i].SetSelectSlot(false); // ���õ��� ���� �������� ����
        }
    }
}
