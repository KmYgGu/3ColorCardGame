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

    private int slotIndex;// 이 값을 통해 버튼 자신이 몇 번째 버튼인지 알고, 클릭하면 이 버튼의 번호로 현재 덱 정보를 말함
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
