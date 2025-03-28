using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class UITextExplain : MonoBehaviour
{
    public static Action<int> OnButtonClicked;

    private TextMeshProUGUI TMP;
    private RectTransform rect;

    private void OnEnable()
    {
        OnButtonClicked += CardTextBox;
    }
    private void OnDisable()
    {
        OnButtonClicked -= CardTextBox;
    }

    private void Awake()
    {
        transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out TMP);
        TryGetComponent<RectTransform>(out rect);
    }

    // 캐시된 설명 데이터를 저장하는 리스트
    private List<string> cachedColorCardExplanations = new List<string>();
    private List<string> cachedEventCardExplanations = new List<string>();

    private void Start()
    {
        CacheCardData();
    }
    // 미리 캐싱을 해서 텍스트를 변화하는 데 걸리는 시간을 줄이기
    private void CacheCardData()
    {
        // Color 카드 설명 캐싱
        cachedColorCardExplanations.Clear();  // 이전 캐시 클리어
        foreach (var colorCard in CardDataManager.Inst.DICColorCardData.Values)
        {
            cachedColorCardExplanations.Add(colorCard.explanation);
        }

        // Event 카드 설명 캐싱
        cachedEventCardExplanations.Clear();  // 이전 캐시 클리어
        foreach (var eventCard in CardDataManager.Inst.DICEventCardData.Values)
        {
            cachedEventCardExplanations.Add(eventCard.explanation);
        }
    }

    void CardTextBox(int index)
    {
     
        string explanation = string.Empty;

        // Color 카드 설명을 캐시에서 가져오기
        if (index < cachedColorCardExplanations.Count)
        {
            explanation = cachedColorCardExplanations[index];
        }
        // Event 카드 설명을 캐시에서 가져오기
        else if (index - CardDataManager.Inst.DICColorCardData.Count < cachedEventCardExplanations.Count)
        {
            explanation = cachedEventCardExplanations[index - CardDataManager.Inst.DICColorCardData.Count];
        }

        // 텍스트에 설명 업데이트
        TMP.text = explanation;
        ResizeTextBox();

    }

    void ResizeTextBox()
    {
        // 텍스트 내용에 맞게 텍스트 박스의 높이를 자동으로 조절
        //TMP.ForceMeshUpdate(); // 텍스트가 변경된 후 즉시 갱신
        float preferredHeight = TMP.preferredHeight;

        // 텍스트 박스의 높이를 텍스트에 맞게 조정
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, preferredHeight);
    }
}
