using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; // ����� ������ �����ϰ� ���� �б�.

[System.Serializable]// ����ȭ�� �����͸� ���������� ���ڿ��� ������ ��
public class PlayerData
{
    public string nickName;
    public int Packgold;
    public int Decks;// ������ �ִ� ���� ��
    public HaveCardData haveCardData;//���⿡ �ִ� HaveCardStock�� ���� ī�带 ��Ÿ��
    //public DeckCardData deckCardData;
    public List<DeckCardData> deckCardData;
}

// ���� ���� ���ݿ� �ʿ��� ��Ÿ�� �����͸� ����
// ����   /   �ҷ�����
// �κ��丮�� �����ϸ鼭 ������ ����
// �÷��̾� ĳ������ ����
public class GameManager : SingleTon<GameManager>
{
    // ������ ���� > int, float ���
    // ������ ���� > ����� ���� Ŭ���� Ÿ�� > ������
    private PlayerData pData;// = new PlayerData();//���� �޸𸮸� �Ҵ����� ������ ������ ����
    public PlayerData PData
    {
        get => pData;
    }

    private int selectingDeckNo;// = 1; // �������� �� ��ȣ
    public int SELECTINGDeckno
    {
        get => selectingDeckNo;
        set => selectingDeckNo = value;
    }
    
    // ���� ���� ���̺� ���� ����
    protected override void DoAwake()
    {
        base.DoAwake();
        DataStart();
    }
    private void DataStart()
    {
        dataPath = Application.persistentDataPath + "/Save";
        //DeleteData();

        //LoadData();
        CreateUserData("1ȣ");
        SaveData();

        /*HaveCardStock newcard = new HaveCardStock();
        newcard.cardID = 10;
        newcard.amount = 1;
        newcard.uID = 1;
        GetCard(newcard);*/

    }

    // �ű� ����
    public void CreateUserData(string newNickName)
    {
        pData = new PlayerData();
        pData.nickName = newNickName;
        pData.Packgold = 1000;
        pData.Decks = 1;
        //pData.Decks = 3;

        pData.haveCardData = new HaveCardData();
        SELECTINGDeckno = 0;
        //pData.deckCardData[SELECTINGDeckno] = new DeckCardData();

        // �� ���� ��ŭ �ݺ�?
        pData.deckCardData = new List<DeckCardData>();

        pData.deckCardData.Add(new DeckCardData());
    }


    #region _Save&Load_

    // ����� > AppData > LocalLow > DefalutCompany > ������Ʈ��
    private string dataPath;
    // ������ �����͸� ���Ϸ� ����
    public void SaveData()
    {
        // ����Ƽ ��Ƽ�÷��� ������ �� �ֵ���,
      
        string data = JsonUtility.ToJson(pData);

        // ��ȣȭ

        File.WriteAllText(dataPath, data);
    }

    // ���Ϸ� ����� �����͸� ������ ��ü�� �ҷ�����
    public bool LoadData()
    {
        if (File.Exists(dataPath))
        {
            string data = File.ReadAllText(dataPath);
            //��ȣȭ
            //pData = new PlayerData();


            pData = JsonUtility.FromJson<PlayerData>(data);
            return true;
        }

        return false;
    }
    // ����� ������ ����
    public void DeleteData()
    {
        File.Delete(dataPath);
    }

    // ���̺� ������ �ִ��� Ȯ���ϰ�, ���ӳ� �����͸� �ε�.
    public bool TryGetPlayerData()
    {
        if (File.Exists(dataPath))
            return LoadData();  // ������ �ε� ����
        return false;// ������ �ε� ����
    }
    #endregion

    #region _CardGet
    public bool GetCard(HaveCardStock newCard)
    {
        pData.haveCardData.AddCard(newCard);
        //Debug.Log("���ο� ī�� �߰�");

        //if (CardDataManager.Inst.GetColorCardData(newCard.cardID, out colorCardData_Entity ColorCardData))
            //Debug.Log(ColorCardData.name);
        return true;

        // ��ø �����۽��� ����
    }
    #endregion

    public void DeckinCard(int uicardindex)//������ �ȿ� ����
    {
        if(uicardindex < CardDataManager.Inst.DICColorCardData.Count) // �÷� ī���� ���
        {
            colorCardData_Entity colorCardDE;
            colorCardDE = CardDataManager.Inst.ReturnColorCardTable(uicardindex);

            DeckCardStock newcard = new DeckCardStock();
            newcard.cardID = colorCardDE.no;
            newcard.amount = 5;
            newcard.uID = 1;

            newcard.isColorCard = true;
            newcard.ownisme = true;

            pData.deckCardData[selectingDeckNo].AddCard(newcard);
        }
        else// �̺�Ʈ ī���� ���
        {
            eventCardData_Entity eventCardDE;
            eventCardDE = CardDataManager.Inst.ReturnEventCardTable(uicardindex- CardDataManager.Inst.DICColorCardData.Count);

            DeckCardStock newcard = new DeckCardStock();
            newcard.cardID = eventCardDE.no;
            newcard.amount = 1;
            newcard.uID = 1;

            newcard.isColorCard = false;
            newcard.ownisme = true;

            pData.deckCardData[selectingDeckNo].AddCard(newcard);
        }
        
    }

    // ����� ������ ���� �ϴ� �� ���� ���� ������ ������ ������ֱ�
    public HaveCardData HCDATA
    {
        get => pData.haveCardData;
        //get => PData.haveCardData;
    }

    public DeckCardData DCDATA
    {
        get => pData.deckCardData[selectingDeckNo];
    }

    public void GetSELECTINGDeckno(int index)
    {
        SELECTINGDeckno = index;
    }

    public void AddDeckSlot()
    {
        //pData.deckCardData = new List<DeckCardData>();

        pData.deckCardData.Add(new DeckCardData());
        Debug.Log(pData.deckCardData.Count);
    }

    // ���� �ʿ��� ��� ���� ���� ������ֱ�
}
