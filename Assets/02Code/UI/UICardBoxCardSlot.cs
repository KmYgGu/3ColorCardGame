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

    public void OnClick_Select()// 카드 박스에서 해당 카드를 클릭했을 때
    {
        if (!EMPTY)
        {
            Debug.Log("자식 스크립트에서 실행, 카드를 클릭함");
            ISSelect = !ISSelect;
            selectedImg.enabled = ISSelect;
        }

    }
}
