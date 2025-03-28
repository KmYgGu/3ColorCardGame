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
                        
            GameManager.Inst.DeckinCard(SLOTINDEX);// 카드 추가를 시작

            DeckUI.OnButtonClicked?.Invoke(SLOTINDEX);

            UITextExplain.OnButtonClicked?.Invoke(SLOTINDEX);
        }

    }

    // 이 카드(버튼 슬롯)가 덱 안에 있으면 선택 이미지를 활성화, 이는 덱 안의 카드가 화면을 갱신할 때마다 델리게이트를 받음
    private void ThisCardinDeck()
    {
        // 이 버튼 슬롯이 가진 SLOTINDEX로 카드 정보를 가져오기

        //GameManager.Inst.DCDATA.DECKcards.Exists<>
        
    }
}
