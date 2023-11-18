using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlowingButton : HighlitedButton
{
    [SerializeField] Transform button;

    [SerializeField] MeshRenderer myRenderer;

    [ColorUsage(true, true)]
    [SerializeField] Color GlowOn;

    [ColorUsage(true, true)]
    [SerializeField] Color NoGlow;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        HoverGlow(GlowOn);
        button.DOLocalMoveZ(0.2f, 0.2f);
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        HoverGlow(NoGlow);
        button.DOLocalMoveZ(0.111f, 0.3f);
    }

    public void HoverGlow(Color Glow)
    {
        myRenderer.material.SetColor("_EmissionColor", Glow);
    }
}
