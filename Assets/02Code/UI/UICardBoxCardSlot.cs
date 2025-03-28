using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UICardBoxCardSlot : UICardSlot
{
    public Button button;

    

    private void Awake()
    {
        base.Awake();
        if (transform.GetChild(1).TryGetComponent<Button>(out button))
            button.onClick.AddListener(OnClick_Select);

    }

    public void OnClick_Select()// ī�� �ڽ����� �ش� ī�带 Ŭ������ ��
    {
        if (!EMPTY)
        {
            
            ISSelect = !ISSelect;// ���� ī�尡 �ִ��� ������ ó�� ISSelect �ʱ�ȭ�� �޶���
            selectedImg.enabled = ISSelect;
                        
            GameManager.Inst.DeckinCard(SLOTINDEX);// ī�� �߰��� ����

            DeckUI.OnButtonClicked?.Invoke(SLOTINDEX);

            UITextExplain.OnButtonClicked?.Invoke(SLOTINDEX);
        }

    }

    // �� ī��(��ư ����)�� �� �ȿ� ������ ���� �̹����� Ȱ��ȭ, �̴� �� ���� ī�尡 ȭ���� ������ ������ ��������Ʈ�� ����
    private void ThisCardinDeck()
    {
        // �� ��ư ������ ���� SLOTINDEX�� ī�� ������ ��������

        //GameManager.Inst.DCDATA.DECKcards.Exists<>
        
    }
}
