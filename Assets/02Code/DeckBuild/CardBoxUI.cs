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

    private List<UICardSlot> slots = new List<UICardSlot>();// ���߿� �ٽ� ���ľ���
    private UICardSlot slot;

    private int currentCount;// ���� � ���������
    private int maxCount;   //�� ������ ���� �� �ִ���

    private List<HaveCardStock> cardList; // ���� ������ ������ �ִ� ī�� ���


    private void Awake()
    {
        InitSlot();
    }
    // ī�� ������ �����ϰ� �����ȭ
    private void InitSlot()
    {
        maxCount = 20;// ���߿� CardDataManager���� �� ī���� ������ �����ؾ���
        for (int i = 0; i < maxCount; i++)
        {
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UICardSlot>(out slot))//   �����԰� ���ÿ� �� ������Ʈ�� ������Ʈ ��������
            {
                slot.SLOTINDEX = 1;
                slots.Add(slot);
            }
            else
                Debug.Log("cardBoxUI���� InitSlot() ���� ���� ����");
        }
    }

    // ���� ������ ī����� �����ؼ� ī�� ������ ����
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
            else // ��ĭ ����
            {
                slots[i].ClearSlot();
            }

            //slots[i].SetSelectSlot(false); // ���õ��� ���� �������� ����
        }
    }
}
