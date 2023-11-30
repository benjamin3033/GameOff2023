using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOcclusion : MonoBehaviour
{
    public List<Renderer> renderers = new();

    public bool ShowOnStart = false;

    private void Start()
    {
        if(renderers.Count > 0)
        ShowRenderers(ShowOnStart);
    }

    public void ShowRenderers(bool show)
    {
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].enabled = show;
        }
    }
}
