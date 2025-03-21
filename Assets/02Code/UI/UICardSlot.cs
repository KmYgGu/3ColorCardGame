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

    private int slotIndex;
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }

    protected Image selectedImg;
    private Image cardimg;
    //public Button button;
    private Transform btnTrans;
    [SerializeField] private TextMeshProUGUI amount;

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
    public virtual void DrawCardSlot(HaveCardStock havecardData)//������ �Ŵ������� ��������
    {
        if (havecardData.cardID % 10 == 0)// ī���� ���� ��ȣ ���� �ڸ��� 0�̸� �÷� ī��
        {
            if (CardDataManager.Inst.GetColorCardData(havecardData.cardID, out colorCardData_Entity cardInfo))
            {
                //Debug.Log(cardInfo.cardicon);
                // ���� �ε��� ���� icon�� ����

                cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                cardimg.enabled = true;
                ChangeAmount(havecardData.amount);  // �������� ����
                isEmpty = false; // ������� ������ false
            }
            else
            {
                Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {havecardData.cardID}");
            }
        }
        else if (havecardData.cardID % 10 == 1)// ī���� ���� ��ȣ ���� �ڸ��� 1�̸� �̺�Ʈ ī��
        {
            if (CardDataManager.Inst.GetEventCardData(havecardData.cardID, out eventCardData_Entity cardInfo))
            {

                cardimg.sprite = Resources.Load<Sprite>(cardInfo.cardicon);// ���̺� ���� ���ϰ�θ� ������� ���� �ε�
                cardimg.enabled = true;
                ChangeAmount(havecardData.amount);  // �������� ����
                isEmpty = false; // ������� ������ false
            }
            else
            {
                Debug.Log($"UICardSlot. ���̺� ���� ī�� �Դϴ�. {havecardData.cardID}");
            }
        }

    }

    private  void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public virtual void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        //cardimg.enabled = false;
        Color imgColor = cardimg.color;
        imgColor.a = 0.5f;
        cardimg.color = imgColor;
        amount.enabled = false;

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
