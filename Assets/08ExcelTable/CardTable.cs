using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]// 이 클래스를 직렬화로 만들어 익스펙터에서 수정이 가능
public class colorCardData_Entity
{
    public int no;
    public string type;
    public string name;
    public string explanation;
    public string cardicon;
}

[System.Serializable]
public class eventCardData_Entity
{
    public int no;
    public string type;
    public string name;
    public string explanation;
    public string cardicon;
}

[System.Serializable]
public class CPUDeckData_Entity
{
    public string DeckName;
    public int card1;
    public int card2;
    public int card3;
    public int card4;
    public int card5;
    public int card6;
    public int card7;
    public int card8;
    public int card9;
    public int card10;
    public int card11;
    public int card12;
    public int card13;
    public int card14;
    public int card15;
    public int card16;
    public int card17;
    public int card18;
    public int card19;
    public int card20;


    // 덱을 리스트로 반환
    public List<int> ToCardList()
    {
        List<int> CPUDeckcardList = new List<int>();

        for (int i = 1; i <= 20; i++)
        {
            var field = this.GetType().GetField($"card{i}");
            if (field != null)
            {
                int cardValue = (int)field.GetValue(this);
                CPUDeckcardList.Add(cardValue);
            }
        }

        return CPUDeckcardList;
    }
}
