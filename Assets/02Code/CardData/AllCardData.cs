using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCardStock//�������� ����
{
    public int cardID;  // ���̺� �������� ID
    //public int amount;
    //public int uID;     // ��ġ�� �ʴ� �������� ���� ID
}
public class AllCardData : SingleTon<AllCardData>//MonoBehaviour
{
    //private int maxItemSlot = CardDataManager.Inst.DICColorCardData.Count + CardDataManager.Inst.DICEventCardData.Count;

    [SerializeField]private List<AllCardStock> Allcard = new List<AllCardStock>();

    private void Start()
    {
        AddAllCard();
    }
    protected override void DoAwake()
    {
        //base.DoAwake();
        //AddAllCard();
    }
    // �����ϴ� ���
    public void AddAllCard()
    {
        //int index = FindCardIndex(newCard);// �� ��° ���Կ� �ִ� ī������ �˻�
        AllCardStock allcardstock = new AllCardStock();


        for (int i = 0; i < CardDataManager.Inst.DICColorCardData.Count; i++)
        {
            allcardstock.cardID = CardDataManager.Inst.ReturnColorCardTable(i).no;

            Allcard.Add(allcardstock);
        }
        for (int i = 0; i < CardDataManager.Inst.DICEventCardData.Count; i++)
        {
            allcardstock.cardID = CardDataManager.Inst.ReturnEventCardTable(i).no;

            Allcard.Add(allcardstock);
        }
        //Debug.Log(Allcard.Count);

    }
    // UI�� ǥ���ϱ� ���ؼ� �ܺο��� �����͸� ����
    public List<AllCardStock> GetCardList()
    {
        //CurItemCount = items.Count;
        return Allcard;
    }
    
    
}
