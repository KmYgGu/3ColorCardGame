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

    private List<UIDeckCardSlot> slots = new List<UIDeckCardSlot>();
    private UIDeckCardSlot slot;
        
    private int maxCount;   //�� ������ ���� �� �ִ���
    private int spaceslot; // ����ִ� ������ ǥ��

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
        spaceslot = maxCount;

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
        // ���� ui��ư�� ��ȣ�� ���� ī���� �ѹ��� ��������
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
                spaceslot++;
                //Debug.Log("�ش� ī�� ����!");
                cardFound = true;

                //break;  // �� ���Ը� �����ϸ� �ȴٸ� break; ���� ���� ���� ó�� �ݺ�
            }

        }

        // �ش� ī�� ��ȣ�� ���� ������ ������ �߰�
        if (!cardFound)
        {
            int looptime;// = 0;

            // ���� ���� ī�尡 �÷� ī������ �̺�Ʈ ī������ Ȯ��
            // �÷� ī���̸� 5�� �߰�
            if (AllCardData.Inst.isColorCard(index))// �� üũ�� ī�忡 �ֱ� ���� Ȯ���ؾ���
            {
                // 5�� �߰��� �ϸ� ���� 20���� �Ѿ�� �� Ȯ��(�̹� ������ ���Ժ��� ������ ������ �ʿ��� ��)
                looptime = 5;
                if(looptime > spaceslot)
                {
                    Debug.Log("�� ������ �ְ� �� ī��� ���� �����ϴ�");
                    return;
                }
            }
            // �̺�Ʈ ī���̸� ���常 �߰�
            else
            {
                looptime = 1;
            }

            for (int j = 0; j < looptime; j++)
            {
                // �� ���� ã��
                int emptyIndex = -1;
                for (int i = 0; i < slots.Count; i++)
                {

                    if (slots[i].SLOTCARDNO < 1) // ������ ����ִٸ�
                    {
                        emptyIndex = i;
                        break;
                    }
                }
                if (emptyIndex != -1)
                {
                    slots[emptyIndex].DrawCardSlot(cardList[index]);
                    GameManager.Inst.DCDATA.CurDeckCount++;
                    spaceslot--;
                    //Debug.Log("ī�� �߰�!");
                    /*if (cardList[emptyIndex].cardID > -1)
                    {

                        slots[emptyIndex].DrawCardSlot(cardList[index]);
                        GameManager.Inst.DCDATA.CurDeckCount++;
                        Debug.Log("ī�� �߰�!");
                    }
                    else
                    {
                        Debug.Log("��ȿ���� ���� ī���Դϴ�.");
                    }*/
                }
                else
                {
                    Debug.Log("������ ���� á���ϴ�.");
                }
            }

            

        }
        //Debug.Log(spaceslot);
        ReorderDeckUI();

    }

    private void ReorderDeckUI()
    {
        
        slots = slots.OrderBy(slot => slot.SLOTCARDNO == 0 ? int.MaxValue : slot.SLOTCARDNO).ToList();

        //  ���ĵ� ���� UI ����
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }
}
