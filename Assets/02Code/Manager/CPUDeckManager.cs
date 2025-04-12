using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUDeckManager : MonoBehaviour
{
    [SerializeField] private CardList cardList;

    //지정한 인덱스의 덱에서 카드 번호 리스트를 가져오기
    public List<int> GetDeckCardList(int deckIndex)
    {
        // 덱 인덱스가 0보다 작거나, 리스트의 길이보다 크거나 같으면
        if (deckIndex < 0 || deckIndex >= cardList.CPUDeckData.Count)
        {
            Debug.LogError($"CPUDeckManager 잘못된 덱 인덱스입니다");

            //아예 새로 만들기
            return new List<int>();
        }

        //CardTable에 있는 덱데이터에 있는 카드 고유번호 리스트를 가져오기
        return cardList.CPUDeckData[deckIndex].ToCardList();
    }
}
