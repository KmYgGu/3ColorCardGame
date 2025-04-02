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

    private int slotIndex;// �̰��� ���� ��ư �ڽ��� �� ��° ��ư���� �˱�
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    private int slotcardno;
    public int SLOTCARDNO// �� ��ư�� �� ���� ī�带 ������ �ִ���
    {
        get => slotcardno;
        set => slotcardno = value;
    }

    protected Image selectedImg;
    private Image cardimg;
    //public Button button;
    private Transform btnTrans;
    private TextMeshProUGUI amount;

    private bool isSelect;
    public bool ISSelect
    {
        get => isSelect;
        set => isSelect = value;
    }
   

    protected virtual void Awake()
    {
        transform.GetChild(0).TryGetComponent<Image>(out selectedImg);
        transform.GetChild(1).TryGetComponent<Image>(out cardimg);

        transform.GetChild(1).TryGetComponent<Transform>(out btnTrans);
        btnTrans.GetChild(0).TryGetComponent<TextMeshProUGUI>(out amount);// �ٸ� UI ��ҵ��� RayCast Ÿ���� �������� ��
                
    }
    //ī�� ���� ����
    public virtual void DrawCardSlot<T>(T havecardData)//������ �Ŵ������� ��������
    {
        isSelect = true;
        //cardimg.enabled = false;
        Color imgColor = cardimg.color;
        imgColor.a = 1f;
        cardimg.color = imgColor;
        amount.enabled = false;

        if (havecardData is HaveCardStock cardStock)
        {
            if (cardStock.cardID % 10 == 0)// ī���� ���� ��ȣ ���� �ڸ��� 0�̸� �÷� ī��
            {
                if (CardDataManager.Inst.GetColorCardData(cardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // ���� �ε��� ���� icon�� ����
                    slotcardno = cardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(cardStock.amount);  // �������� ����
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {cardStock.cardID}");
                }
            }
            else if (cardStock.cardID % 10 == 1)// ī���� ���� ��ȣ ���� �ڸ��� 1�̸� �̺�Ʈ ī��
            {
                if (CardDataManager.Inst.GetEventCardData(cardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = cardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(cardStock.amount);  // �������� ����
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {cardStock.cardID}");
                }
            }
        }
        else if (havecardData is AllCardStock allCardStock)
        {
            if (allCardStock.cardID % 10 == 0)// ī���� ���� ��ȣ ���� �ڸ��� 0�̸� �÷� ī��
            {
                if (CardDataManager.Inst.GetColorCardData(allCardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // ���� �ε��� ���� icon�� ����
                    slotcardno = allCardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(0);  // �������� ����
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {allCardStock.cardID}");
                }
            }
            else if (allCardStock.cardID % 10 == 1)// ī���� ���� ��ȣ ���� �ڸ��� 1�̸� �̺�Ʈ ī��
            {
                if (CardDataManager.Inst.GetEventCardData(allCardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = allCardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(0);  
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {allCardStock.cardID}");
                }
            }
        }

        else if (havecardData is DeckCardStock DeckCardStock)
        {
            if (DeckCardStock.cardID % 10 == 0)// ī���� ���� ��ȣ ���� �ڸ��� 0�̸� �÷� ī��
            {
                if (CardDataManager.Inst.GetColorCardData(DeckCardStock.cardID, out colorCardData_Entity cardInfo))
                {
                    //Debug.Log(cardInfo.cardicon);
                    // ���� �ε��� ���� icon�� ����
                    slotcardno = DeckCardStock.cardID;


                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(0);  // �������� ����
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {DeckCardStock.cardID}");
                }
            }
            else if (DeckCardStock.cardID % 10 == 1)// ī���� ���� ��ȣ ���� �ڸ��� 1�̸� �̺�Ʈ ī��
            {
                if (CardDataManager.Inst.GetEventCardData(DeckCardStock.cardID, out eventCardData_Entity cardInfo))
                {
                    slotcardno = DeckCardStock.cardID;

                    cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                    cardimg.enabled = true;
                    ChangeAmount(0);
                    isEmpty = false; // ������� ������ false
                }
                else
                {
                    Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {DeckCardStock.cardID}");
                }
            }
        }

    }
        

    private void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public virtual void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        //cardimg.enabled = false;
        cardimg.sprite = null;
        Color imgColor = cardimg.color;
        imgColor.a = 0.5f;
        cardimg.color = imgColor;
        amount.enabled = false;
        slotcardno = 0;
        //gameObject.SetActive(false);
    }

    // ���� ������ ����
    public virtual void ChangeAmount(int newAnount)
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
