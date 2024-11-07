using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsDisplay : MonoBehaviour
{
    private TMP_Text _text;
    private void Awake()
    {
        _text = gameObject.GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
    
    public IEnumerator FadeIn(float duration)
    {
        if (_text == null) yield break;

        float elapsedTime = 0f;
        Color color = _text.color;
        color.a = 0;
        _text.color = color;
        
        while (elapsedTime < duration)
        {
            if (_text == null) yield break;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / duration);
            _text.color = color;
            yield return null;
        }
    }
        
    public IEnumerator FadeOut(float duration)
    {
        if (_text == null) yield break;

        float elapsedTime = 0f;
        Color color = _text.color;
        
        while (elapsedTime < duration)
        {
            if (_text == null) yield break;
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(1 - (elapsedTime / duration));
            _text.color = color;
            yield return null;
        }
    }
}
