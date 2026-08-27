using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingNumber : PoolableObject
{
    [SerializeField] float textDurationSeconds = 1f; // How long the text actually exists.
    [SerializeField] float textFloatDurationSeconds = .6f; // How long the text floats until stopping
    [SerializeField] float textFloatHeight = 1f;
    [SerializeField] float textYOffset = 0.25f;
    [SerializeField] float textFadeEndValue = 0.1f;
    [SerializeField] public TextMeshProUGUI text;
    void OnEnable()
    {
    }
    public void initText(string contents, Vector3 pos)
    {
        text.alpha = 1;
        text.text = contents;
        transform.position = new Vector3(pos.x, pos.y + textYOffset, pos.z);
        transform.DOMoveY(transform.position.y + textFloatHeight, 0.6f).SetEase(Ease.OutCubic);
        text.DOFade(textFadeEndValue, textDurationSeconds).OnComplete( () => { objectPool.Release(this); } );

    }

}
