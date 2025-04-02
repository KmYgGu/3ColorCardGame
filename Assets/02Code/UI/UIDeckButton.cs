using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDeckButton : MonoBehaviour
{
    private bool isEmpty;
    public bool EMPTY
    {
        get => isEmpty;
    }

    private int slotIndex;// �� ���� ���� ��ư �ڽ��� �� ��° ��ư���� �˰�, Ŭ���ϸ� �� ��ư�� ��ȣ�� ���� �� ������ ����
    public int SLOTINDEX
    {
        get => slotIndex;
        set => slotIndex = value;
    }
    

    private TextMeshProUGUI DeckNameText;
    private Button button;

    private void Awake()
    {
        TryGetComponent<Button>(out button);
        transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out DeckNameText);

        button.onClick.AddListener(OnClick_Select);
    }

    private void OnClick_Select()
    {
        GameManager.Inst.GetSELECTINGDeckno(slotIndex);
        //Debug.Log(slotIndex);
        DeckUI.OnDeckbtnClicked?.Invoke();
    }
}
