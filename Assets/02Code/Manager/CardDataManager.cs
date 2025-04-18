using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataManager : SingleTon<CardDataManager> //MonoBehaviour
{
    [SerializeField]CardList cardList;

    private Dictionary<int, colorCardData_Entity> dicColorcardData = new Dictionary<int, colorCardData_Entity>();
    private Dictionary<int, eventCardData_Entity> dicEventcardData = new Dictionary<int, eventCardData_Entity>();

    public Dictionary<int, colorCardData_Entity> DICColorCardData
    {
        get => dicColorcardData;
    }

    public Dictionary<int, eventCardData_Entity> DICEventCardData
    {
        get => dicEventcardData;
    }

    protected override void DoAwake()
    {
        base.DoAwake();
        MakecardDic();
    }

    void MakecardDic()
    {
        cardList = Resources.Load<CardList>("CardList");//리소스 폴더 기준, 마지막엔 오브젝트명도 붙여야함

        for (int i = 0; i < cardList.colorCardData.Count; i++)
        {
            dicColorcardData.Add(cardList.colorCardData[i].no, cardList.colorCardData[i]);
        }

        for (int i = 0; i < cardList.eventCardData.Count; i++)
        {
            dicEventcardData.Add(cardList.eventCardData[i].no, cardList.eventCardData[i]);
        }
    }


    public bool GetColorCardData(int No, out colorCardData_Entity ColorCardData)
    {
        return dicColorcardData.TryGetValue(No, out ColorCardData);
    }

    public bool GetEventCardData(int No, out eventCardData_Entity EventCardData)
    {
        return dicEventcardData.TryGetValue(No, out EventCardData);
    }

    // 값을 기반으로 테이블을 반환
    public colorCardData_Entity ReturnColorCardTable(int index)
    {
        return cardList.colorCardData[index];
    }

    public eventCardData_Entity ReturnEventCardTable(int index)
    {
        return cardList.eventCardData[index];
    }
}
