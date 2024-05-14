using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayListText : MonoBehaviour
{
    private List<TextMeshProUGUI> _textMeshProUguis = new List<TextMeshProUGUI>();

    private void Awake()
    {
        int count = gameObject.transform.childCount;
        for (int i = 0; i< count; i++)
        {
            GameObject text = gameObject.transform.GetChild(i).gameObject;
            text.TryGetComponent<TextMeshProUGUI>(out TextMeshProUGUI textMeshProUgui);
            _textMeshProUguis.Add(textMeshProUgui);
        }
    }

    public void AlphaTextAnimation(int value, Action action)
    {
        TextMeshProUGUI text = _textMeshProUguis[value];
        Color color = text.color;
        DOTween.To(() => color.a, x =>
        {
            color.a = x;
            text.color = color;
        }, 1, 0.5f).OnComplete(() => {
        DOTween.To(() => color.a, x =>
        {
            color.a = x;
            text.color = color;
        }, 0, 0.5f).OnComplete(() =>
        {
            if(value+1 < _textMeshProUguis.Count) AlphaTextAnimation(value+1, action);
            else action?.Invoke();
        });});
    }
    public IEnumerator AlphaCoroutine(Action action)
    {
        foreach (var text in _textMeshProUguis)
        {
            Color color = text.color;
            while (color.a < 1)
            {
                color.a += 0.1f;
                text.color = color;
                yield return new WaitForSeconds(0.001f);
            }
            while (color.a > 0)
            {
                color.a -= 0.1f;
                text.color = color;
                yield return new WaitForSeconds(0.001f);
            }
        }
        action?.Invoke();
        yield return null;
        
    }
}
