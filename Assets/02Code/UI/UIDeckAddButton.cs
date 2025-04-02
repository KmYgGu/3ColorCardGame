using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeckAddButton : MonoBehaviour
{
    private Button Button;

    private void Awake()
    {
        TryGetComponent<Button>(out Button);
        Button.onClick.AddListener(OnClick_Select);
    }

    private void OnClick_Select()
    {
        //��������Ʈ ����
        UIDeckSlotContent.OnButtonClicked?.Invoke();

        //GameManager���� �� ���� �߰� �Լ� ����
        GameManager.Inst.AddDeckSlot();
    }

    // �� �ż��� �ִ�ġ �̻��̸� �� ������Ʈ�� ��Ȱ��ȭ�ϱ�
}
