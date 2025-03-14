using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWhosDeck
{
    
    void CheckDeck(GameObject who, int index);// 이 덱의 소유자가 누구인지, 또 덱의 갯수는 몇 장인지

    void CheckDeckColorCard(int index);

    void CheckDeckEventCard(int index);
}
