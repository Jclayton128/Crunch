using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingVisualSorter : MonoBehaviour
{
    private enum DispLayer { ForeNear, Fore, BackClose, Back}

    //state
    DispLayer _dp;

    private void Start()
    {
        SnapYPos();
        AdjustDisplayLayer();
    }

    private void SnapYPos()
    {
        if (transform.position.y < 0.0625f)
        {
            _dp = DispLayer.ForeNear;
            transform.position = new Vector2(transform.position.x, 0f);
            return;
        }
        if (transform.position.y < 0.125f)
        {
            _dp = DispLayer.Fore;
            transform.position = new Vector2(transform.position.x, 0.0625f);
            return;
        }
        if (transform.position.y > 0.13f)
        {
            _dp = DispLayer.BackClose;
            transform.position = new Vector2(transform.position.x, 0.1875f);
            return;
        }
        if (transform.position.y > 0.1875f)
        {
            _dp = DispLayer.Back;
            transform.position = new Vector2(transform.position.x, 0.25f);
            return;
        }
    }

    private void AdjustDisplayLayer()
    {
        if (_dp == DispLayer.ForeNear || _dp == DispLayer.Fore)
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingLayerName = "Ground";
        }
    }
}
