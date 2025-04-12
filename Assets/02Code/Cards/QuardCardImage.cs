using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuardCardImage : MonoBehaviour
{
    private MeshRenderer imageRenderer; // 자식 Quad
    //public Texture2D[] cardTextures;// 나중에 리소스 폴더를 참고하는 것으로 변경
    private Texture2D texture2D;

    private MaterialPropertyBlock mpb;//Draw call를 최적화

    private void Awake()
    {
        TryGetComponent<MeshRenderer>(out imageRenderer);
        mpb = new MaterialPropertyBlock();
    }
    private void Start()
    {
        // 코루틴을 통해 GameManager의 덱 데이터가 초기화되었는지 확인한 후 진행
        StartCoroutine(WaitForDeckInitialization());
    }

    IEnumerator WaitForDeckInitialization()
    {
        // 예제: 첫 번째 덱의 이름이 null이 아닐 때까지 기다림
        yield return new WaitUntil(() => !string.IsNullOrEmpty(GameManager.Inst.PData.deckCardData[0].DeckName));

        // 초기화가 완료된 후에 카드 이미지 설정
        SetCardImage(16);// 0번째(1번째 카드)
    }
    public void SetCardImage(int index)
    {
        // 싱글톤 게임매니저가 가지고 있는 덱 정보의 카드 하나를 가져오기
        DeckCardStock DCD = GameManager.Inst.DCDATA.DECKcards[index];

        // 이 카드가 컬러 카드인지 확인
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
