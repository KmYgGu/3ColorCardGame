using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // 저장소 파일을 생성하고 쓰고 읽기.

[System.Serializable]// 직렬화한 데이터만 정상적으로 문자열로 저장이 됨
public class PlayerData
{
    public string nickName;
    public int Packgold;
    public int Decks;
    public HaveCardData haveCardData;//여기에 있는 HaveCardStock가 소유 카드를 나타냄
    public DeckCardData deckCardData;
}

// 게임 구동 전반에 필요한 런타임 데이터를 관리
// 저장   /   불러오기
// 인벤토리를 관리하면서 아이템 관리
// 플레이어 캐릭터의 정보
public class GameManager : SingleTon<GameManager>
{
    // 원시형 변수 > int, float 등등
    // 참조형 변수 > 사용자 정의 클래스 타입 > 포인터
    private PlayerData pData;// = new PlayerData();//동적 메모리를 할당하지 않으면 쓸수가 없다
    public PlayerData PData
    {
        get => pData;
    }
    
    // 기존 유저 세이브 파일 생성
    protected override void DoAwake()
    {
        base.DoAwake();
        DataStart();
    }
    private void DataStart()
    {
        dataPath = Application.persistentDataPath + "/Save";
        //DeleteData();

        LoadData();
        //CreateUserData("1호");
        //SaveData();

        /*HaveCardStock newcard = new HaveCardStock();
        newcard.cardID = 10;
        newcard.amount = 1;
        newcard.uID = 1;
        GetCard(newcard);*/

        //SetAllTableCard();

        //Debug.Log(pData.haveCardData.GetCardList());

        //string strData = JsonUtility.ToJson(pData);
    }

    // 신규 유저
    public void CreateUserData(string newNickName)
    {
        pData = new PlayerData();
        pData.nickName = newNickName;
        pData.Packgold = 1000;
        pData.Decks = 1;

        pData.haveCardData = new HaveCardData();
        pData.deckCardData = new DeckCardData();
    }

    #region _Save&Load_

    // 사용자 > AppData > LocalLow > DefalutCompany > 프로젝트명
    private string dataPath;
    // 게임의 데이터를 파일로 저장
    public void SaveData()
    {
        // 유니티 멀티플랫폼 구동될 수 있도록,
      
        string data = JsonUtility.ToJson(pData);

        // 암호화

        File.WriteAllText(dataPath, data);
    }

    // 파일로 저장된 데이터를 게임의 객체로 불러오는
    public bool LoadData()
    {
        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            //복호화
            //pData = new PlayerData();


            pData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }

        return false;
    }
    // 저장된 파일을 삭제
    public void DeleteData()
    {
        File.Delete(dataPath);
    }

    // 세이브 파일이 있는지 확인하고, 게임내 데이터를 로드.
    public bool TryGetPlayerData()
    {
        if (File.Exists(dataPath))
            return LoadData();  // 데이터 로드 성공
        return false;// 데이터 로드 실패
    }
    #endregion

    #region _CardGet
    public bool GetCard(HaveCardStock newCard)
    {
        pData.haveCardData.AddCard(newCard);
        //Debug.Log("새로운 카드 추가");

        //if (CardDataManager.Inst.GetColorCardData(newCard.cardID, out colorCardData_Entity ColorCardData))
            //Debug.Log(ColorCardData.name);
        return true;

        // 중첩 아이템습득 로직
    }
    #endregion

    public void DeckinCard(int uicardindex)//삭제는 안에 있음
    {
        if(uicardindex < CardDataManager.Inst.DICColorCardData.Count)
        {
            colorCardData_Entity colorCardDE;
            colorCardDE = CardDataManager.Inst.ReturnColorCardTable(uicardindex);

            DeckCardStock newcard = new DeckCardStock();
            newcard.cardID = colorCardDE.no;
            newcard.amount = 5;
            newcard.uID = 1;

            pData.deckCardData.AddCard(newcard);
        }
        else
        {
            //Debug.Log("이벤트 카드 입니다");
            eventCardData_Entity eventCardDE;
            eventCardDE = CardDataManager.Inst.ReturnEventCardTable(uicardindex- CardDataManager.Inst.DICColorCardData.Count);

            DeckCardStock newcard = new DeckCardStock();
            newcard.cardID = eventCardDE.no;
            newcard.amount = 1;
            newcard.uID = 1;

            pData.deckCardData.AddCard(newcard);
        }
        
    }

    // 대놓고 변수를 참조 하는 것 보단 따로 참조할 변수를 만들어주기
    public HaveCardData HCDATA
    {
        get => pData.haveCardData;
        //get => PData.haveCardData;
    }

    public DeckCardData DCDATA
    {
        get => pData.deckCardData;
    }

    // 추후 필요할 경우 겟터 셋터 만들어주기
}
