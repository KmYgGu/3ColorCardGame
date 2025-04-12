using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuardCardImage : MonoBehaviour
{
    private MeshRenderer imageRenderer; // �ڽ� Quad
    //public Texture2D[] cardTextures;// ���߿� ���ҽ� ������ �����ϴ� ������ ����
    private Texture2D texture2D;

    private MaterialPropertyBlock mpb;//Draw call�� ����ȭ

    private void Awake()
    {
        TryGetComponent<MeshRenderer>(out imageRenderer);
        mpb = new MaterialPropertyBlock();
    }
    private void Start()
    {
        // �ڷ�ƾ�� ���� GameManager�� �� �����Ͱ� �ʱ�ȭ�Ǿ����� Ȯ���� �� ����
        StartCoroutine(WaitForDeckInitialization());
    }

    IEnumerator WaitForDeckInitialization()
    {
        // ����: ù ��° ���� �̸��� null�� �ƴ� ������ ��ٸ�
        yield return new WaitUntil(() => !string.IsNullOrEmpty(GameManager.Inst.PData.deckCardData[0].DeckName));

        // �ʱ�ȭ�� �Ϸ�� �Ŀ� ī�� �̹��� ����
        SetCardImage(16);// 0��°(1��° ī��)
    }
    public void SetCardImage(int index)
    {
        // �̱��� ���ӸŴ����� ������ �ִ� �� ������ ī�� �ϳ��� ��������
        DeckCardStock DCD = GameManager.Inst.DCDATA.DECKcards[index];

        // �� ī�尡 �÷� ī������ Ȯ��
        if (DCD.isColorCard)
        {
            if (CardDataManager.Inst.GetColorCardData(DCD.cardID, out colorCardData_Entity cardInfo))
            {
                texture2D = Resources.Load<Texture2D>(cardInfo.cardicon);
            }
        }
        else
        {
            if (CardDataManager.Inst.GetEventCardData(DCD.cardID, out eventCardData_Entity cardInfo))
            {
                texture2D = Resources.Load<Texture2D>(cardInfo.cardicon);
            }
        }
        


        mpb.SetTexture("_MainTex", texture2D);
        imageRenderer.SetPropertyBlock(mpb);
    }
}
