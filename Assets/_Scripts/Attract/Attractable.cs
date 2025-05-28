using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractable : MonoBehaviour
{
    [SerializeField]
    private bool rotateToCenter;
    [SerializeField]
    Attractor currentAttractor;

    private Transform _trans;
    private Collider2D _coll;
    private Rigidbody2D _rg;

    private void Awake()
    {
        _trans = GetComponent<Transform>();
        _rg = GetComponent<Rigidbody2D>();
        _coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (currentAttractor != null)
        {
            if(!currentAttractor.attractedObjects.Contains(_coll))
                currentAttractor = null;
            if (rotateToCenter)
                RotateCenter();
        }
    }


    public void Attract(Attractor planet)
    {
        Vector2 attractionDir = (Vector2)planet.planetTrans.position - _rg.position;
        _rg.AddForce(attractionDir.normalized * Time.fixedDeltaTime * -planet.gravity * 100);

        if(currentAttractor == null)
        {
            currentAttractor = planet;
        }
    }


    public void RotateCenter()
    {
        Vector2 distanceVector = (Vector2)currentAttractor.planetTrans.position - (Vector2)_trans.position;
        float angle = Mathf.Atan2(distanceVector.x, distanceVector.y) * Mathf.Rad2Deg;
        _trans.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}