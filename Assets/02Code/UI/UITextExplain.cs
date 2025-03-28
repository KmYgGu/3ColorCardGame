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

    // ĳ�õ� ���� �����͸� �����ϴ� ����Ʈ
    private List<string> cachedColorCardExplanations = new List<string>();
    private List<string> cachedEventCardExplanations = new List<string>();

    private void Start()
    {
        CacheCardData();
    }
    // �̸� ĳ���� �ؼ� �ؽ�Ʈ�� ��ȭ�ϴ� �� �ɸ��� �ð��� ���̱�
    private void CacheCardData()
    {
        // Color ī�� ���� ĳ��
        cachedColorCardExplanations.Clear();  // ���� ĳ�� Ŭ����
        foreach (var colorCard in CardDataManager.Inst.DICColorCardData.Values)
        {
            cachedColorCardExplanations.Add(colorCard.explanation);
        }

        // Event ī�� ���� ĳ��
        cachedEventCardExplanations.Clear();  // ���� ĳ�� Ŭ����
        foreach (var eventCard in CardDataManager.Inst.DICEventCardData.Values)
        {
            cachedEventCardExplanations.Add(eventCard.explanation);
        }
    }

    void CardTextBox(int index)
    {
     
        string explanation = string.Empty;

        // Color ī�� ������ ĳ�ÿ��� ��������
        if (index < cachedColorCardExplanations.Count)
        {
            explanation = cachedColorCardExplanations[index];
        }
        // Event ī�� ������ ĳ�ÿ��� ��������
        else if (index - CardDataManager.Inst.DICColorCardData.Count < cachedEventCardExplanations.Count)
        {
            explanation = cachedEventCardExplanations[index - CardDataManager.Inst.DICColorCardData.Count];
        }

        // �ؽ�Ʈ�� ���� ������Ʈ
        TMP.text = explanation;
        ResizeTextBox();

    }

    void ResizeTextBox()
    {
        // �ؽ�Ʈ ���뿡 �°� �ؽ�Ʈ �ڽ��� ���̸� �ڵ����� ����
        //TMP.ForceMeshUpdate(); // �ؽ�Ʈ�� ����� �� ��� ����
        float preferredHeight = TMP.preferredHeight;

        // �ؽ�Ʈ �ڽ��� ���̸� �ؽ�Ʈ�� �°� ����
        rect.sizeDelta = new Vector2(rect.sizeDelta.x, preferredHeight);
    }
}
