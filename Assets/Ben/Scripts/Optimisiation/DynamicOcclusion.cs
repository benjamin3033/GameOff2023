using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicOcclusion : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<TileOcclusion>(out TileOcclusion tile))
        {
            tile.ShowRenderers(true);
        }

        if(other.TryGetComponent<SkinnedOcclusion>(out SkinnedOcclusion renderer))
        {
            renderer.ShowRenderers(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<TileOcclusion>(out TileOcclusion tile))
        {
            tile.ShowRenderers(false);
        }

        if (other.TryGetComponent<SkinnedOcclusion>(out SkinnedOcclusion renderer))
        {
            renderer.ShowRenderers(false);
        }
    }
}
