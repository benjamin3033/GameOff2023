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

    [SerializeField] float EnterAmount = 0.2f;
    [SerializeField] float ExitAmount = 0.111f;

    public override void OnPointerEnter(PointerEventData eventData)
    {
        HoverGlow(GlowOn);
        button.DOLocalMoveZ(EnterAmount, 0.2f);
    }

    public override void OnPointerExit(PointerEventData eventData) 
    {
        HoverGlow(NoGlow);
        button.DOLocalMoveZ(ExitAmount, 0.3f);
    }

    public void HoverGlow(Color Glow)
    {
        myRenderer.material.SetColor("_EmissionColor", Glow);
    }
}
