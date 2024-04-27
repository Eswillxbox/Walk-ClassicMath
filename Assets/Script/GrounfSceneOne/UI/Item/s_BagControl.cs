using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class s_BagControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Text tips_Text;

    private Vector3 startPosition = new Vector3(800,-491,0);
    private Vector3 endPosition = new Vector3(800, 491, 0);
    private Vector2 originalPosition;
    private RectTransform rectTransform;

    private void Start()
    {
        tips_Text = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();  
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition - eventData.position;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, eventData.position.y + originalPosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //�ж��Ƿ���ק����һ��
        if (Camera.main.WorldToViewportPoint(rectTransform.TransformPoint(rectTransform.anchoredPosition)).y >= 0.5)
        {
            rectTransform.anchoredPosition = endPosition;
            tips_Text.text = "������ק";
        }
        else
        {
            rectTransform.anchoredPosition = startPosition;
            tips_Text.text = "������ק";
        }
    }
}
