using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUDeckManager : MonoBehaviour
{
    [SerializeField] private CardList cardList;

    //������ �ε����� ������ ī�� ��ȣ ����Ʈ�� ��������
    public List<int> GetDeckCardList(int deckIndex)
    {
        // �� �ε����� 0���� �۰ų�, ����Ʈ�� ���̺��� ũ�ų� ������
        if (deckIndex < 0 || deckIndex >= cardList.CPUDeckData.Count)
        {
            Debug.LogError($"CPUDeckManager �߸��� �� �ε����Դϴ�");

            //�ƿ� ���� �����
            return new List<int>();
        }

        //CardTable�� �ִ� �������Ϳ� �ִ� ī�� ������ȣ ����Ʈ�� ��������
        return cardList.CPUDeckData[deckIndex].ToCardList();
    }
}
