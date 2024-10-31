using System;
using DG.Tweening;
using TMPro;

public class DOText
{
    public static void DOTexting(string text, TextMeshProUGUI textCompo, float duration,Ease ease = Ease.Linear ,Action callback = null)
    {
        DOTween.To(() => textCompo.text, x => textCompo.text = x, text, duration
        ).SetEase(ease).OnComplete(() => callback?.Invoke());
    }
}