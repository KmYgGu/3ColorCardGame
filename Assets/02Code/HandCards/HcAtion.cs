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
        

    public void CheckCollectCard()// �� ī�� �߿��� ��� ������ �̺�Ʈ ī�尡 �ְų�, ¦�� �´� �÷� ī�尡 �ִ���
    {
        throw new System.NotImplementedException();
    }

    public void MyHandOverSix()
    {
        if (handCardInfo.HandCardsindex > 5)
        {
            Debug.Log($"�а� 6�� �̻��Դϴ�! ���� �� �� {handCardInfo.HandCardsindex}");
        }
    }
}
