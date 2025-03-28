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
            
            ISSelect = !ISSelect;// 덱에 카드가 있는지 유무로 처음 ISSelect 초기화가 달라짐
            selectedImg.enabled = ISSelect;

            //Debug.Log(SLOTINDEX);
            GameManager.Inst.DeckinCard(SLOTINDEX);

            DeckUI.OnButtonClicked?.Invoke(SLOTINDEX);
        }

    }
}
