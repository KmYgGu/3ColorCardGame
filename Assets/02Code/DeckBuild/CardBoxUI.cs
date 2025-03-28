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

    CardList allcardList;

    private List<UICardBoxCardSlot> slots = new List<UICardBoxCardSlot>();// �� ����
    private UICardBoxCardSlot slot;

    private int currentCount;// ���� � ���������
    private int maxCount;   //�� ������ ���� �� �ִ���

    //private List<HaveCardStock> cardList; // ���� ������ ������ �ִ� ī�� ���    ..����
    private List<AllCardStock> allcardStock;

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
        allcardList = Resources.Load<CardList>("CardList");
        //maxCount = CardDataManager.Inst.DICColorCardData.Count + CardDataManager.Inst.DICEventCardData.Count;//7;// ���߿� CardDataManager���� �� ī���� ������ �����ؾ���

        maxCount = allcardList.colorCardData.Count + allcardList.eventCardData.Count;
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

        //cardList = GameManager.Inst.HCDATA.GetCardList();
        //currentCount = GameManager.Inst.HCDATA.CurItemCount;

        allcardStock = AllCardData.Inst.GetCardList();

        for (int i = 0; i < maxCount; i++)
        {

            /*if (i < currentCount && cardList[i].cardID > -1)// �����ϰ� �ִ� ī���� ���� ���� ���� �ִ� ī�� ������ ����, �� ī�� ���̵� 0���� Ŭ ��
            {
                slots[i].DrawCardSlot(cardList[i]);
            }
            else // ��ĭ ����
            {
                slots[i].ClearSlot();
            }*/

            /*if(slots[i].SLOTINDEX < allcardList.colorCardData.Count)
            {
                if (allcardList.colorCardData[i].no > -1)// �����ϰ� �ִ� ī���� ���� ���� ���� �ִ� ī�� ������ ����, �� ī�� ���̵� 0���� Ŭ ��
                {
                    slots[i].DrawCardSlot(allcardStock[i]);
                }
                else // ��ĭ ����
                {
                    slots[i].ClearSlot();
                }
            }
            else
            {
                if (allcardList.eventCardData[i].no > -1)// �����ϰ� �ִ� ī���� ���� ���� ���� �ִ� ī�� ������ ����, �� ī�� ���̵� 0���� Ŭ ��
                {
                    slots[i].DrawCardSlot(allcardList.eventCardData[i]);
                }
                else // ��ĭ ����
                {
                    slots[i].ClearSlot();
                }
            }*/

            if (allcardStock[i].cardID > -1)// �����ϰ� �ִ� ī���� ���� ���� ���� �ִ� ī�� ������ ����, �� ī�� ���̵� 0���� Ŭ ��
            {
                //Debug.Log(i+" ��° " +allcardStock[i].cardID);
                slots[i].DrawCardSlot(allcardStock[i]);
            }
            else // ��ĭ ����
            {
                slots[i].ClearSlot();
            }



            //slots[i].SetSelectSlot(false); // ���õ��� ���� �������� ����
        }
    }
}
