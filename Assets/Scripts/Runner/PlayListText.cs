using System;
using System.Collections;
using System.Collections.Generic;
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
