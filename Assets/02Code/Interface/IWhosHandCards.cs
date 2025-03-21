using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWhosHandCards 
{
    void CheckHandCards(GameObject who, int index);// 이 덱의 소유자가 누구인지, 또 패의 갯수는 몇 장인지

    void CheckHandColorCard(int index);// 자기 패중 컬러 카드가 몇 장인지

    void CheckHandEventCard(int index);// 자기 패중 이벤트 카드가 몇 장인지
}
