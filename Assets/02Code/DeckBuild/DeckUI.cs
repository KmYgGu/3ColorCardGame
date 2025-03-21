using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� �����Ҷ� ī��ui ������Ʈ�� ����
// ī�带 ��ų� ���� ������, ������ ������� �ؼ� ui������ ����
public class DeckUI : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private RectTransform contentTrans;

    private List<UIDeckCardSlot> slots = new List<UIDeckCardSlot>();// �� ����
    private UIDeckCardSlot slot;

    private int currentCount;// ���� � ���������
    private int maxCount;   //�� ������ ���� �� �ִ���

    private List<HaveCardStock> cardList; // ���� ������ ������ �ִ� ī�� ���    ..����


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
        maxCount = 7;// ���߿� CardDataManager���� �� ī���� ������ �����ؾ���
        for (int i = 0; i < maxCount; i++)
        {
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UIDeckCardSlot>(out slot))
            {
                slot.SLOTINDEX = 1;
                slots.Add(slot);
            }
            else
                Debug.Log("DeckUI���� InitSlot() ���� ���� ����");
        }
        RefreshCardBoxUI();
    }

    // ���� ������ ī����� �����ؼ� ī�� ������ ����
    public void RefreshCardBoxUI()
    {

        cardList = GameManager.Inst.HCDATA.GetCardList();
        currentCount = GameManager.Inst.HCDATA.CurItemCount;

        //maxCount = GameManager.Inst.HCDATA

        for (int i = 0; i < maxCount; i++)
        {
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
