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

    private Image selectedImg;
    private Image cardimg;
    public Button button;
    private bool isSelect;

    private TextMeshPro amount;

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent<Image>(out selectedImg);
        transform.GetChild(1).TryGetComponent<Image>(out cardimg);
        transform.GetChild(2).TryGetComponent<TextMeshPro>(out amount);

        if (transform.GetChild(1).TryGetComponent<Button>(out button))
            button.onClick.AddListener(OnClick_Select);

    }
    //ī�� ���� ����
    public void DrawCardSlot(HaveCardStock havecardData)//������ �Ŵ������� ��������
    {
        // �÷�ī�� �ΰ��
        if(CardDataManager.Inst.GetColorCardData(havecardData.cardID, out colorCardData_Entity cardInfo))
        {
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

    public void OnClick_Select()//���Ŀ� ����
    {
        if (!isEmpty)
        {
            Debug.Log("ī�带 Ŭ����");
            isSelect = !isSelect;
            selectedImg.enabled = isSelect;
        }
        
    }

    private void Start()
    {
        selectedImg.enabled = false;
        isSelect = false;
    }

    public void ClearSlot()
    {
        isSelect = false;
        isEmpty = true;
        cardimg.enabled = false;
        amount.enabled = false;

        gameObject.SetActive(false);
    }

    // ���� ������ ����
    public void ChangeAmount(int newAnount)
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
