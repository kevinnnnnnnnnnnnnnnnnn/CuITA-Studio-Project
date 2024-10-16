using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    public PolygonCollider2D newBounds;

    private void Awake()
    {
        newBounds = GetComponent<PolygonCollider2D>();
    }


    private void Start()
    {
        BoundsManager.instance.UpdateBounds(newBounds);
    }
}
