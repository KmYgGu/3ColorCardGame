using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWhosHandCards 
{
    void CheckHandCards(GameObject who, int index);// �� ���� �����ڰ� ��������, �� ���� ������ �� ������

    void CheckHandColorCard(int index);// �ڱ� ���� �÷� ī�尡 �� ������

    void CheckHandEventCard(int index);// �ڱ� ���� �̺�Ʈ ī�尡 �� ������
}
