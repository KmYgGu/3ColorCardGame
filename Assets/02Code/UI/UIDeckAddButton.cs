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
        //델리게이트 실행
        UIDeckSlotContent.OnButtonClicked?.Invoke();
    }

    // 덱 매수가 최대치 이상이면 이 오브젝트를 비활성화하기
}
