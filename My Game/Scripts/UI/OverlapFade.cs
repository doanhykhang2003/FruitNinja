using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Unity.VisualScripting;

public class OverlapFade : BaseOverlap
{
    [SerializeField]
    private Image imgFade;
    [SerializeField]
    private Color fadeColor;

    public override void Init()
    {
        base.Init();
    }

    public override void Show(object data)
    {
        base.Show(data);
    }

    public override void Hide()
    {
        base.Hide();
    }

    private void SetAlpha(float alp)
    {
        Color cl = this.imgFade.color;
        cl.a = alp;
        this.imgFade.color = cl;
    }

    public void Fade(float fadeTime, Action onDuringFade = null, Action onFinish = null)
    {
        imgFade.color = fadeColor;
        SetAlpha(0);

        DG.Tweening.Sequence seq = DOTween.Sequence();

        seq.Append(this.imgFade.DOFade(1f, fadeTime)); //fade-in
        seq.AppendCallback(() =>
        {
            onDuringFade?.Invoke();
        });
        seq.Append(this.imgFade.DOFade(0, fadeTime)); //fade-out
        seq.OnComplete(() =>
        {
            onFinish?.Invoke();
            this.Hide();
        });
    }
}
