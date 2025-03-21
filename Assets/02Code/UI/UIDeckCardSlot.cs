using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckCardSlot : UICardSlot
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
            
        }

    }
}
