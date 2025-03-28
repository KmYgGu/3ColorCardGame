using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

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

    private List<AllCardStock> cardList; // ��� ī�� ������

    public static Action<int> OnButtonClicked;

    private void OnEnable()
    {
        OnButtonClicked += RefreshCardBoxUI;
    }
    private void OnDisable()
    {
        OnButtonClicked -= RefreshCardBoxUI;
    }

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
        
        maxCount = 20;// ���߿� CardDataManager���� �� ī���� ������ �����ؾ���
        for (int i = 0; i < maxCount; i++)
        {
            if (Instantiate(cardPrefab, contentTrans).TryGetComponent<UIDeckCardSlot>(out slot))
            {
                slot.SLOTINDEX = i;
                //slot.EMPTY =
                slots.Add(slot);
            }
            else
                Debug.Log("DeckUI���� InitSlot() ���� ���� ����");
        }

        //RefreshCardBoxUI();
    }

    // ���� ������ ī����� �����ؼ� ī�� ������ ����
    public void RefreshCardBoxUI(int index)
    {
        int slotcardno;
        if(index < CardDataManager.Inst.DICColorCardData.Count)
        {
            slotcardno = CardDataManager.Inst.ReturnColorCardTable(index).no;
        }
        else
        {
            slotcardno = CardDataManager.Inst.ReturnEventCardTable(index - CardDataManager.Inst.DICColorCardData.Count).no;
        }

        cardList = AllCardData.Inst.GetCardList();
        //currentCount = GameManager.Inst.DCDATA.CurDeckCount;
      
        bool cardFound = false;

        // �̹� ������ ���Ե鿡�� �ش� ī�� ��ȣ�� ���� ������ �ִ��� Ȯ��
        for (int i = 0; i < slots.Count; i++)
        {
            // slot�� cardID�� cardNumber�� ���ٸ� �ش� ���Կ� ī�尡 �ִٴ� �ǹ�
            if (slots[i].SLOTCARDNO == slotcardno)
            {
                slots[i].ClearSlot();  // ���� Ŭ����: ī�� ����
                //slots.RemoveAt(i);

                GameManager.Inst.DCDATA.CurDeckCount--;  // ���� ��ϵ� ī�� �� ����
                Debug.Log("�ش� ī�� ����!");
                cardFound = true;

                //break;  // �� ���Ը� �����ϸ� �ȴٸ� break; ���� ���� ���� ó�� �ݺ�
            }
        }

        // �ش� ī�� ��ȣ�� ���� ������ ������ �߰�
        if (!cardFound)
        {
            /*currentCount = GameManager.Inst.DCDATA.CurDeckCount;

            if (currentCount >= slots.Count)
            {
                Debug.Log("������ ���� á���ϴ�.");
                return;
            }
            slots[currentCount].DrawCardSlot(cardList[index]);
                        
            GameManager.Inst.DCDATA.CurDeckCount++;*/

            // �� ���� ã��
            int emptyIndex = -1;
            for (int i = 0; i < slots.Count; i++)
            {
                Debug.Log(slots[i].SLOTCARDNO);
                if (slots[i].SLOTCARDNO < 1) // ������ ����ִٸ�
                {
                    emptyIndex = i;
                    break;
                }
            }
            if (emptyIndex != -1)
            {
                
                if (cardList[emptyIndex].cardID > -1)
                {
                    
                    slots[emptyIndex].DrawCardSlot(cardList[index]);
                    GameManager.Inst.DCDATA.CurDeckCount++;
                    Debug.Log("ī�� �߰�!");
                }
                else
                {
                    Debug.Log("��ȿ���� ���� ī���Դϴ�.");
                }
            }
            else
            {
                Debug.Log("������ ���� á���ϴ�.");
            }

        }
        ReorderDeckUI();

        // ���� ���� ���� �� ����(���� ī�� ���� ��ġ)�� �� ī�� �߰�
        //if (currentCount < slots.Count && cardList[index].cardID > -1)
        //{
        //    slots[currentCount].DrawCardSlot(cardList[index]);


        //    // ���� �߰��� ��, ���� �� ������ �������Ѿ� �Ѵٸ� ���⼭ ó��
        //    GameManager.Inst.DCDATA.CurDeckCount++;
        //}
        //else
        //{
        //    Debug.Log("������ ���� á�ų�, ��ȿ���� ���� ī���Դϴ�.");
        //}

    }

    private void ReorderDeckUI()
    {
        /*slots.Sort((a, b) =>
        {
            if (a.SLOTCARDNO == 0) return 1;  // �� ����(a)�� �ڷ� �̵�
            if (b.SLOTCARDNO == 0) return -1; // �� ����(b)�� �ڷ� �̵�
            return a.SLOTCARDNO.CompareTo(b.SLOTCARDNO); // ī�� ��ȣ �������� ����
        });*/
        slots = slots.OrderBy(slot => slot.SLOTCARDNO == 0 ? int.MaxValue : slot.SLOTCARDNO).ToList();

        //  ���ĵ� ���� UI ����
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }
}
