using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HcAtion : MonoBehaviour, IHandCard
{
    HandCardInfo handCardInfo;

    private void Awake()
    {
        TryGetComponent<HandCardInfo>(out handCardInfo);
    }
        

    public void CheckCollectCard()// 내 카드 중에서 사용 가능한 이벤트 카드가 있거나, 짝이 맞는 컬러 카드가 있는지
    {
        throw new System.NotImplementedException();
    }

    public void MyHandOverSix()
    {
        if (handCardInfo.HandCardsindex > 5)
        {
            Debug.Log($"패가 6장 이상입니다! 현재 패 수 {handCardInfo.HandCardsindex}");
        }
    }
}
