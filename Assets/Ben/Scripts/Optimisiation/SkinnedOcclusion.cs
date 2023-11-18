using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinnedOcclusion : MonoBehaviour
{
    public List<SkinnedMeshRenderer> renderers = new();

    public void ShowRenderers(bool show)
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].enabled = show;
        }
    }
}
