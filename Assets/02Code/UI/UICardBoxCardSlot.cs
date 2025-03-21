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
            Debug.Log("�ڽ� ��ũ��Ʈ���� ����, ī�带 Ŭ����");
            ISSelect = !ISSelect;
            selectedImg.enabled = ISSelect;
        }

    }
}
