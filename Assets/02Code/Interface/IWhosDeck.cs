using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWhosDeck
{
    
    void CheckDeck(GameObject who, int index);// �� ���� �����ڰ� ��������, �� ���� ������ �� ������

    void CheckDeckColorCard(int index);

    void CheckDeckEventCard(int index);
}
